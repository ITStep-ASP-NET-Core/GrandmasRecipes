using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IIngredientRepository
    {
        Task<ICollection<Ingredient>> GetIngredientsByRecipeIdAsync ( Guid recipeId );

        Task AddIngredientAsync ( Ingredient ingredient );
		void UpdateIngredient ( Ingredient ingredient );
		void DeleteIngredient ( Ingredient ingredient );
	}
}