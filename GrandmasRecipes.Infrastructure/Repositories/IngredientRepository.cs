using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class IngredientRepository : IIngredientRepository
	{
        private readonly ApplicationContext _context;

        public IngredientRepository(ApplicationContext context)
        {
            _context = context;
        }

		public async Task<ICollection<Ingredient>> GetIngredientsByRecipeIdAsync ( Guid recipeId )
		{
			return await _context.Ingredients.Where(i => i.RecipeId == recipeId).Include(i => i.Product).AsNoTracking().ToListAsync();
		}

		public async Task AddIngredientAsync ( Ingredient ingredient )
		{
			await _context.Ingredients.AddAsync(ingredient);
		}

		public void UpdateIngredient ( Ingredient ingredient )
		{
			_context.Ingredients.Update(ingredient);
		}

		public void DeleteIngredient ( Ingredient ingredient )
		{
			_context.Ingredients.Remove(ingredient);
		}
	}
}