using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task AddAsync(Product entity);
        void UpdateAsync(Product entity);
        void DeleteAsync(Product entity);
        Task SaveChangesAsync();
    }
}