using GrandmasRecipes.Application.Common;
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

        public async Task<Account?> GetAccountByIdAsync(Guid id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<PagedResult<Account>> GetAccountsAsync(int page, int pageSize = 10)
        {
            var query = _context.Accounts;

            var items = await query
                .Skip(page * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResult<Account>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = await query.CountAsync()
            };
        }

        public async Task AddAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public void UpdateAccount(Account account)
        {
            _context.Accounts.Update(account);
        }

        public void DeleteAccount(Account account)
        {
            _context.Accounts.Remove(account);
        }
    }
}