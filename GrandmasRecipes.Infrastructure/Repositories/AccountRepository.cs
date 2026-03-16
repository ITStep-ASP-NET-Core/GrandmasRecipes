using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data; // 
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository // 
    {
        private readonly ApplicationContext _context;

        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Account> GetAll() => _context.Set<Account>().AsNoTracking();

        public async Task<Account?> GetByIdAsync(Guid id) => await _context.Set<Account>().FindAsync(id);

        public async Task AddAsync(Account entity) => await _context.Set<Account>().AddAsync(entity);

        public void Update(Account entity) => _context.Set<Account>().Update(entity);

        public void Delete(Account entity) => _context.Set<Account>().Remove(entity);
    }
}