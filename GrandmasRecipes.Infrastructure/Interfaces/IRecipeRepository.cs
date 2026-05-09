using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> GetAllAsync();
        Task<Recipe?> GetByIdAsync(Guid id);
        Task AddAsync(Recipe entity);
        void UpdateAsync(Recipe entity);
        void DeleteAsync(Recipe entity);
        Task SaveChangesAsync();
        IQueryable<Recipe> GetRecipesByAuthorAsync(Guid authorId);
        Task<PagedResult<Recipe>> GetByLikesPagedAsync(int page, int pageSize = 20);
        Task<Recipe?> GetRecipeDetailsAsync(Guid id);
    }
}