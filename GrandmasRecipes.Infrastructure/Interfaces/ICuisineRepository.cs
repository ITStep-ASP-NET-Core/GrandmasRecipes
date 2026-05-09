using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface ICuisineRepository
    {
        IQueryable<Cuisine> GetAllAsync();
        Task<Cuisine?> GetByIdAsync(int id);
        Task AddAsync(Cuisine entity);
        void UpdateAsync(Cuisine entity);
        void DeleteAsync(Cuisine entity);
        Task SaveChangesAsync();
    }
}