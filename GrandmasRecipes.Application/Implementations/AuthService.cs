using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GrandmasRecipes.Application.DTO.Auth;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GrandmasRecipes.Application.Implementations
{
	public class AuthService (
		IAccountRepository accountRepository,
		IRefreshTokenRepository refreshTokenRepository,
		IConfiguration configuration ) : IAuthService
	{
		public async Task<AuthResponseDTO> RegisterAsync ( RegisterDto dto )
		{
			var existing = await accountRepository.GetByEmailAsync(dto.Email);
			if(existing is not null)
				throw new InvalidOperationException("Email already in use.");

			var user = new User
			{
				Id = Guid.NewGuid(),
				Nickname = dto.Nickname,
				Email = dto.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
				RegisteredAt = DateTime.UtcNow
			};

			await accountRepository.CreateAsync(user);
			await accountRepository.SaveChangesAsync();

			return await GenerateAuthResponseAsync(user);
		}

		public async Task<AuthResponseDTO> LoginAsync ( LoginDto dto )
		{
			var account = await accountRepository.GetByEmailAsync(dto.Email);
			if(account is null || !BCrypt.Net.BCrypt.Verify(dto.Password, account.PasswordHash))
				throw new UnauthorizedAccessException("Invalid email or password.");

			return await GenerateAuthResponseAsync(account);
		}

		public async Task<AuthResponseDTO> RefreshAsync ( string refreshToken )
		{
			var token = await refreshTokenRepository.GetByTokenAsync(refreshToken);
			if(token is null || token.ExpiresAt < DateTime.UtcNow)
				throw new UnauthorizedAccessException("Invalid or expired refresh token.");

			await refreshTokenRepository.RevokeAsync(token);
			await refreshTokenRepository.SaveChangesAsync();

			return await GenerateAuthResponseAsync(token.Account!);
		}

		public async Task LogoutAsync ( string refreshToken )
		{
			var token = await refreshTokenRepository.GetByTokenAsync(refreshToken);
			if(token is null)
				return;

			await refreshTokenRepository.RevokeAsync(token);
			await refreshTokenRepository.SaveChangesAsync();
		}

		private async Task<AuthResponseDTO> GenerateAuthResponseAsync ( Account account )
		{
			var accessToken = GenerateAccessToken(account);
			var refreshToken = await CreateRefreshTokenAsync(account.Id);

			return new AuthResponseDTO
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				Nickname = account.Nickname,
				AccountId = account.Id
			};
		}

		private string GenerateAccessToken ( Account account )
		{
			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!));

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
				new Claim(ClaimTypes.Email, account.Email),
				new Claim(ClaimTypes.Name, account.Nickname)
			};

			var token = new JwtSecurityToken(
				issuer: configuration["Jwt:Issuer"],
				audience: configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(15),
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private async Task<string> CreateRefreshTokenAsync ( Guid accountId )
		{
			var tokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

			var refreshToken = new RefreshToken
			{
				Token = tokenValue,
				AccountId = accountId,
				ExpiresAt = DateTime.UtcNow.AddDays(7),
				IsRevoked = false
			};

			await refreshTokenRepository.CreateAsync(refreshToken);
			await refreshTokenRepository.SaveChangesAsync();

			return tokenValue;
		}
	}
}