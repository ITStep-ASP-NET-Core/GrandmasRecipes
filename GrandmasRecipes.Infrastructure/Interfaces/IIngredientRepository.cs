using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    /// <summary>
    /// Репозиторій для роботи з інгредієнтами рецептів.
    /// </summary>
    public interface IIngredientRepository
    {
        /// <summary>
        /// Отримати всі інгредієнти конкретного рецепту.
        /// </summary>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        Task<ICollection<Ingredient>> GetIngredientsByRecipeIdAsync(Guid recipeId);

        /// <summary>Додати інгредієнт до рецепту.</summary>
        /// <param name="ingredient">Інгредієнт для додавання.</param>
        Task AddIngredientAsync(Ingredient ingredient);

        /// <summary>Оновити інгредієнт.</summary>
        /// <param name="ingredient">Інгредієнт з оновленими даними.</param>
        void UpdateIngredient(Ingredient ingredient);

        /// <summary>Видалити інгредієнт.</summary>
        /// <param name="ingredient">Інгредієнт для видалення.</param>
        void DeleteIngredient(Ingredient ingredient);
    }
}