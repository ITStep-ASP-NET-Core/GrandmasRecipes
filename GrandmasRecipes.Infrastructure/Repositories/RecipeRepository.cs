using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationContext _context;

        public RecipeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Recipe> GetAll() => _context.Recipes.AsNoTracking();
        public async Task<Recipe?> GetByIdAsync(Guid id) => await _context.Recipes.FindAsync(id);
        public async Task AddAsync(Recipe entity) => await _context.Recipes.AddAsync(entity);
        public void Update(Recipe entity) => _context.Recipes.Update(entity);
        public void Delete(Recipe entity) => _context.Recipes.Remove(entity);

        public IQueryable<Recipe> GetRecipesWithAuthors()
            => _context.Recipes.Include(r => r.Author).AsNoTracking();

        public IQueryable<Recipe> GetRecipesByAuthor(Guid authorId)
            => _context.Recipes.Where(r => r.AuthorId == authorId).Include(r => r.Author).AsNoTracking();
    }
}