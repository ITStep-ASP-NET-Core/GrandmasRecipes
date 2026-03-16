using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        // Реализуй только методы для Аккаунтов (Login, Register и т.д.)
        // Удаляй отсюда всё, что связано с GetRecipesWithAuthors!
    }
}