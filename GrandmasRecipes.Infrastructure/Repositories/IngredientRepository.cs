using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationContext _context;

        // Внедряем контекст БД
        public IngredientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> GetByRecipeIdAsync(Guid recipeId)
        {
            return await _context.Ingredients
                .Where(i => i.RecipeId == recipeId)
                .ToListAsync();
        }

        public async Task AddAsync(Ingredient ingredient) => await _context.Ingredients.AddAsync(ingredient);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}