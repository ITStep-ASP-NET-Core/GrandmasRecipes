using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IReviewRepository
    {
        IQueryable<Review> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        IQueryable<Review> GetByRecipeIdAsync(Guid recipeId);
        IQueryable<Review> GetByAccountIdAsync(Guid accountId);
        Task AddAsync(Review entity);
        void UpdateAsync(Review entity);
        void DeleteAsync(Review entity);
        Task SaveChangesAsync();
    }
}