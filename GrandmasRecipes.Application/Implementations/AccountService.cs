using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Account;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<AccountDto> GetAccountAsync(Guid accountId)
        {
            var account = await _uow.Accounts.GetAccountByIdAsync(accountId);
            if (account == null) throw new Exception("Аккаунт не найден");

            return new AccountDto
            {
                Id = account.Id,
                Nickname = account.Nickname,
                ImageUrl = null,
                Likes = account.Liked.Count,
                Published = account.Recipes.Count
            };
        }

        public async Task<Result> EditAccountAsync(AccountEditDto dto)
        {
            var account = await _uow.Accounts.GetAccountByIdAsync(dto.Id);
            if (account == null) return Result.Fail("Аккаунт не найден");

            if (dto.Nickname != null) account.Nickname = dto.Nickname;

            _uow.Accounts.UpdateAccount(account);
            await _uow.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> DeleteAccountAsync(Guid accountId)
        {
            var account = await _uow.Accounts.GetAccountByIdAsync(accountId);
            if (account == null) return Result.Fail("Аккаунт не найден");

            _uow.Accounts.DeleteAccount(account);
            await _uow.SaveChangesAsync();
            return Result.Ok();
        }
    }
}