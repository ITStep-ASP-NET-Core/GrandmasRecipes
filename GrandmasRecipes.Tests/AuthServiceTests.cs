using GrandmasRecipes.Application.DTO.Auth;
using GrandmasRecipes.Application.Implementations;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace GrandmasRecipes.Tests
{
    public class AuthServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow = new();
        private readonly Mock<IAccountRepository> _accountRepo = new();
        private readonly Mock<IRefreshTokenRepository> _tokenRepo = new();
        private readonly Mock<IPasswordHasher> _hasher = new();
        private readonly AuthService _service;

        public AuthServiceTests()
        {
            _uow.Setup(u => u.Accounts).Returns(_accountRepo.Object);
            _uow.Setup(u => u.RefreshTokens).Returns(_tokenRepo.Object);

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["Jwt:Secret"] = "super-secret-key-that-is-long-enough-32chars",
                    ["Jwt:Issuer"] = "GrandmasRecipes",
                    ["Jwt:Audience"] = "GrandmasRecipesUsers"
                })
                .Build();

            _service = new AuthService(_uow.Object, _hasher.Object, config);
        }

        [Fact]
        public async Task RegisterAsync_Success_ReturnsAuthResponse()
        {
            var dto = new RegisterDto { Nickname = "olga", Email = "olga@test.com", Password = "pass123" };

            _accountRepo.Setup(r => r.GetAccountByEmailAsync(dto.Email)).ReturnsAsync((Account?)null);
            _hasher.Setup(h => h.HashPassword(dto.Password)).Returns("hashed");
            _tokenRepo.Setup(r => r.CreateAsync(It.IsAny<RefreshToken>())).Returns(Task.CompletedTask);

            var result = await _service.RegisterAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("olga", result.Nickname);
            Assert.False(string.IsNullOrEmpty(result.AccessToken));
            Assert.False(string.IsNullOrEmpty(result.RefreshToken));
        }

        [Fact]
        public async Task RegisterAsync_EmailAlreadyInUse_ThrowsInvalidOperationException()
        {
            var dto = new RegisterDto { Email = "olga@test.com", Password = "pass", Nickname = "olga" };
            var existing = new User { Email = dto.Email };

            _accountRepo.Setup(r => r.GetAccountByEmailAsync(dto.Email)).ReturnsAsync(existing);

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterAsync(dto));
        }

        [Fact]
        public async Task LoginAsync_ValidCredentials_ReturnsAuthResponse()
        {
            var dto = new LoginDto { Email = "olga@test.com", Password = "pass123" };
            var account = new User { Id = Guid.NewGuid(), Email = dto.Email, Nickname = "olga", PasswordHash = "hashed" };

            _accountRepo.Setup(r => r.GetAccountByEmailAsync(dto.Email)).ReturnsAsync(account);
            _hasher.Setup(h => h.VerifyPassword(dto.Password, "hashed")).Returns(true);
            _tokenRepo.Setup(r => r.CreateAsync(It.IsAny<RefreshToken>())).Returns(Task.CompletedTask);

            var result = await _service.LoginAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("olga", result.Nickname);
        }

        [Fact]
        public async Task LoginAsync_WrongPassword_ThrowsUnauthorizedAccessException()
        {
            var dto = new LoginDto { Email = "olga@test.com", Password = "wrong" };
            var account = new User { Email = dto.Email, PasswordHash = "hashed" };

            _accountRepo.Setup(r => r.GetAccountByEmailAsync(dto.Email)).ReturnsAsync(account);
            _hasher.Setup(h => h.VerifyPassword(dto.Password, "hashed")).Returns(false);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.LoginAsync(dto));
        }

        [Fact]
        public async Task LoginAsync_EmailNotFound_ThrowsUnauthorizedAccessException()
        {
            var dto = new LoginDto { Email = "notexist@test.com", Password = "pass" };

            _accountRepo.Setup(r => r.GetAccountByEmailAsync(dto.Email)).ReturnsAsync((Account?)null);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.LoginAsync(dto));
        }

        [Fact]
        public async Task RefreshAsync_ExpiredToken_ThrowsUnauthorizedAccessException()
        {
            var expiredToken = new RefreshToken
            {
                Token = "old-token",
                ExpiresAt = DateTime.UtcNow.AddDays(-1),
                Account = new User { Id = Guid.NewGuid(), Nickname = "olga", Email = "olga@test.com" }
            };

            _tokenRepo.Setup(r => r.GetByTokenAsync("old-token")).ReturnsAsync(expiredToken);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.RefreshAsync("old-token"));
        }

        [Fact]
        public async Task RefreshAsync_TokenNotFound_ThrowsUnauthorizedAccessException()
        {
            _tokenRepo.Setup(r => r.GetByTokenAsync("bad-token")).ReturnsAsync((RefreshToken?)null);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.RefreshAsync("bad-token"));
        }

        [Fact]
        public async Task LogoutAsync_TokenNotFound_DoesNotThrow()
        {
            _tokenRepo.Setup(r => r.GetByTokenAsync("bad-token")).ReturnsAsync((RefreshToken?)null);

            var ex = await Record.ExceptionAsync(() => _service.LogoutAsync("bad-token"));
            Assert.Null(ex);
        }

        [Fact]
        public async Task LogoutAsync_ValidToken_RevokesToken()
        {
            var token = new RefreshToken { Token = "valid-token" };
            _tokenRepo.Setup(r => r.GetByTokenAsync("valid-token")).ReturnsAsync(token);
            _tokenRepo.Setup(r => r.RevokeAsync(token)).Returns(Task.CompletedTask);

            await _service.LogoutAsync("valid-token");

            _tokenRepo.Verify(r => r.RevokeAsync(token), Times.Once);
            _uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
