using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;

        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync() => await _context.Accounts.ToListAsync();

        public async Task<Account?> GetByIdAsync(Guid id) => await _context.Accounts.FindAsync(id);

        public async Task AddAsync(Account account) => await _context.Accounts.AddAsync(account);

        public void Update(Account account) => _context.Accounts.Update(account);

        public void Delete(Account account) => _context.Accounts.Remove(account);

        // Реализация сохранения изменений через контекст
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}