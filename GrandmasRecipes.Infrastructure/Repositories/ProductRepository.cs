using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Product> GetAll()
            => _context.Products.AsNoTracking();

        public async Task<Product?> GetByIdAsync(int id)
            => await _context.Products.FindAsync(id);

        public async Task AddAsync(Product entity)
            => await _context.Products.AddAsync(entity);

        public void Update(Product entity)
            => _context.Products.Update(entity);

        public void Delete(Product entity)
            => _context.Products.Remove(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}