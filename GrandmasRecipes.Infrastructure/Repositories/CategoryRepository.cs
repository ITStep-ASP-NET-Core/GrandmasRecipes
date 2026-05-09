using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;

        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll()
            => _context.Categories.AsNoTracking();

        public async Task<Category?> GetByIdAsync(int id)
            => await _context.Categories.FindAsync(id);

        public async Task AddAsync(Category entity)
            => await _context.Categories.AddAsync(entity);

        public void Update(Category entity)
            => _context.Categories.Update(entity);

        public void Delete(Category entity)
            => _context.Categories.Remove(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}