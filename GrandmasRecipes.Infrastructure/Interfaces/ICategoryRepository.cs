using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category entity);
        void UpdateAsync(Category entity);
        void DeleteAsync(Category entity);
        Task SaveChangesAsync();
    }
}