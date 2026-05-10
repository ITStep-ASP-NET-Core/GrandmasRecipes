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
	public class AuthService : IAuthService
	{
		private readonly IUnitOfWork _uow;
		private readonly IPasswordHasher _passwordHasher;
		private readonly IConfiguration _configuration;

		public AuthService ( IUnitOfWork uow, IPasswordHasher passwordHasher, IConfiguration configuration )
		{
			_uow = uow;
			_passwordHasher = passwordHasher;
			_configuration = configuration;
		}

		public async Task<AuthResponseDto> RegisterAsync ( RegisterDto dto )
		{
			var existing = await _uow.Accounts.GetAccountByEmailAsync(dto.Email);
			if(existing is not null)
				throw new InvalidOperationException("Email already in use.");

			var user = new User
			{
				Id = Guid.NewGuid(),
				Nickname = dto.Nickname,
				Email = dto.Email,
				PasswordHash = _passwordHasher.HashPassword(dto.Password),
				RegisteredAt = DateTime.UtcNow
			};

			await _uow.Accounts.AddAccountAsync(user);
			await _uow.SaveChangesAsync();

			return await GenerateAuthResponseAsync(user);
		}

		public async Task<AuthResponseDto> LoginAsync ( LoginDto dto )
		{
			var account = await _uow.Accounts.GetAccountByEmailAsync(dto.Email);
			if(account is null || !_passwordHasher.VerifyPassword(dto.Password, account.PasswordHash))
				throw new UnauthorizedAccessException("Invalid email or password.");

			return await GenerateAuthResponseAsync(account);
		}

		public async Task<AuthResponseDto> RefreshAsync ( string refreshToken )
		{
			var token = await _uow.RefreshTokens.GetByTokenAsync(refreshToken);
			if(token is null || token.ExpiresAt < DateTime.UtcNow)
				throw new UnauthorizedAccessException("Invalid or expired refresh token.");

			await _uow.RefreshTokens.RevokeAsync(token);
			await _uow.SaveChangesAsync();

			return await GenerateAuthResponseAsync(token.Account!);
		}

		public async Task LogoutAsync ( string refreshToken )
		{
			var token = await _uow.RefreshTokens.GetByTokenAsync(refreshToken);
			if(token is null)
				return;

			await _uow.RefreshTokens.RevokeAsync(token);
			await _uow.SaveChangesAsync();
		}

		private async Task<AuthResponseDto> GenerateAuthResponseAsync ( Account account )
		{
			var accessToken = GenerateAccessToken(account);
			var refreshToken = await CreateRefreshTokenAsync(account.Id);

			return new AuthResponseDto
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
				Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!));

			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
				new Claim(ClaimTypes.Email, account.Email),
				new Claim(ClaimTypes.Name, account.Nickname)
			};

			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
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

			await _uow.RefreshTokens.CreateAsync(refreshToken);
			await _uow.SaveChangesAsync();

			return tokenValue;
		}
	}
}