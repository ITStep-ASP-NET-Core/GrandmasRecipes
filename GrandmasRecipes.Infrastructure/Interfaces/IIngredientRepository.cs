using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetByRecipeIdAsync(Guid recipeId);
        Task AddAsync(Ingredient ingredient);
        Task SaveChangesAsync();
    }
}