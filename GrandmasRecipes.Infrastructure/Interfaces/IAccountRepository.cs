using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByIdAsync ( Guid id );

        Task AddAccountAsync ( Account account );
		void UpdateAccount ( Account account );
		void DeleteAccount ( Account account );
    }
}