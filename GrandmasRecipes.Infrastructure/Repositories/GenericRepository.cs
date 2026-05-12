using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

		public async Task<ICollection<T>> GetAllAsync ( )
		{
			return await _dbSet.AsNoTracking().ToListAsync();
		}

		public async Task<T?> GetByIdAsync ( int id )
		{
			return await _dbSet.FindAsync(id);
		}

		public async Task AddAsync ( T obj )
		{
			await _dbSet.AddAsync(obj);
		}

		public void Update ( T obj )
		{
			_dbSet.Update(obj);
		}

		public void Delete ( T obj )
		{
			_dbSet.Remove(obj);
		}
	}
}