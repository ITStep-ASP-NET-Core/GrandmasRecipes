using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(Guid id);
        Task AddAsync(Account account);
        void UpdateAsync(Account account);
        void DeleteAsync(Account account);
        Task SaveChangesAsync();
    }
}