using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(Guid id);
        Task AddAsync(Account account);
        void Update(Account account);
        void Delete(Account account);

        // Метод для записи правок в базу
        Task SaveChangesAsync();
    }
}