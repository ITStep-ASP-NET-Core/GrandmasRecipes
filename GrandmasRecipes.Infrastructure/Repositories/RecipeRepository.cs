using GrandmasRecipes.Application.Common;
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


        public IQueryable<Recipe> GetRecipesByAuthor(Guid authorId)
            => _context.Recipes.Where(r => r.AuthorId == authorId).Include(r => r.Author).AsNoTracking();

        public async Task<PagedResult<Recipe>> GetByLikesPagedAsync(int page, int pageSize = 20)
        {
            var items = await _context.Recipes
                .OrderByDescending(r => r.Likes)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Include(r => r.Author)
                .AsNoTracking()
                .ToListAsync();

            return new PagedResult<Recipe>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = await _context.Recipes.CountAsync()
            };
        }
        public async Task<Recipe?> GetRecipeDetailsAsync(Guid id)
        {
            return await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Cuisine)
                .Include(r => r.Dificulty)
                .Include(r => r.Categories)
                .Include(r => r.Ingredients)
                    .ThenInclude(i => i.Product)
                .Include(r => r.Steps)
                    .ThenInclude(s => s.SubSteps)
                .Include(r => r.Liked)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
       
    }
}