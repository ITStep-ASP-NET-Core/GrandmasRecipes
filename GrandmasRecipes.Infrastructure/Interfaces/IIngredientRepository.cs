using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IIngredientRepository
    {
        // Получить все ингредиенты конкретного рецепта
        Task<IEnumerable<Ingredient>> GetByRecipeIdAsync(Guid recipeId);
        Task AddAsync(Ingredient ingredient);
        Task SaveChangesAsync(); // Чтобы сохранить в БД
    }
}