using GrandmasRecipes.Application.DTO.Account;
using GrandmasRecipes.Application.Implementations;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace GrandmasRecipes.Tests
{
    public class AccountServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow = new();
        private readonly Mock<IAccountRepository> _accountRepo = new();
        private readonly AccountService _service;

        public AccountServiceTests()
        {
            _uow.Setup(u => u.Accounts).Returns(_accountRepo.Object);
            _service = new AccountService(_uow.Object);
        }

        [Fact]
        public async Task GetAccountAsync_AccountExists_ReturnsDto()
        {
            var id = Guid.NewGuid();
            var account = new User
            {
                Id = id,
                Nickname = "olga",
                Liked = new List<Like>(),
                Recipes = new List<Recipe>()
            };

            _accountRepo.Setup(r => r.GetAccountByIdAsync(id)).ReturnsAsync(account);

            var result = await _service.GetAccountAsync(id);

            Assert.NotNull(result);
            Assert.Equal("olga", result.Nickname);
            Assert.Equal(0, result.Likes);
            Assert.Equal(0, result.Published);
        }

        [Fact]
        public async Task GetAccountAsync_NotFound_ThrowsException()
        {
            var id = Guid.NewGuid();
            _accountRepo.Setup(r => r.GetAccountByIdAsync(id)).ReturnsAsync((Account?)null);

            await Assert.ThrowsAsync<Exception>(() => _service.GetAccountAsync(id));
        }

        [Fact]
        public async Task EditAccountAsync_AccountExists_UpdatesNickname()
        {
            var id = Guid.NewGuid();
            var account = new User { Id = id, Nickname = "old_name" };
            var dto = new AccountEditDto { Id = id, Nickname = "new_name" };

            _accountRepo.Setup(r => r.GetAccountByIdAsync(id)).ReturnsAsync(account);

            var result = await _service.EditAccountAsync(dto);

            Assert.True(result.Success);
            Assert.Equal("new_name", account.Nickname);
            _uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditAccountAsync_NotFound_ReturnsFail()
        {
            var dto = new AccountEditDto { Id = Guid.NewGuid(), Nickname = "new" };
            _accountRepo.Setup(r => r.GetAccountByIdAsync(dto.Id)).ReturnsAsync((Account?)null);

            var result = await _service.EditAccountAsync(dto);

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task DeleteAccountAsync_AccountExists_ReturnsSuccess()
        {
            var id = Guid.NewGuid();
            var account = new User { Id = id };

            _accountRepo.Setup(r => r.GetAccountByIdAsync(id)).ReturnsAsync(account);

            var result = await _service.DeleteAccountAsync(id);

            Assert.True(result.Success);
            _accountRepo.Verify(r => r.DeleteAccount(account), Times.Once);
            _uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAccountAsync_NotFound_ReturnsFail()
        {
            var id = Guid.NewGuid();
            _accountRepo.Setup(r => r.GetAccountByIdAsync(id)).ReturnsAsync((Account?)null);

            var result = await _service.DeleteAccountAsync(id);

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
        }
    }
}
