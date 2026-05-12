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

		public RecipeRepository ( ApplicationContext context )
		{
			_context = context;
		}

		public async Task<PagedResult<Recipe>> GetRecipesByLikesAsync ( int page, int pageSize = 10 )
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

		public async Task<PagedResult<Recipe>> GetRecipesByAuthorAsync ( Guid authorId, int page, int pageSize = 10 )
		{
			var query = _context.Recipes.Where(r => r.AuthorId == authorId);

			var items = await query
				.OrderByDescending(r => r.PublishedDate)
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
				TotalCount = await query.CountAsync()
			};
		}

		public async Task<PagedResult<Recipe>> GetLikedRecipesByUserAsync ( Guid userId, int page, int pageSize = 10 )
		{
			var query = _context.Recipes
				.Where(r => _context.Likes.Any(l => l.AccountId == userId && l.RecipeId == r.Id));

			var items = await query
				.OrderByDescending(r => r.PublishedDate)
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
				TotalCount = await query.CountAsync()
			};
		}

		public async Task<PagedResult<Recipe>> GetRecipesByFiltersAsync (
			string? searchQuery,
			ICollection<int>? categoryIds,
			ICollection<int>? cuisineIds,
			ICollection<int>? difficultyIds,
			ICollection<int>? productIds,
			int page,
			int pageSize = 10 )
		{
			var query = _context.Recipes.AsQueryable();

			if(!string.IsNullOrWhiteSpace(searchQuery))
				query = query.Where(r => r.Title.ToLower().Contains(searchQuery.ToLower()));

			if(categoryIds != null && categoryIds.Count > 0)
				query = query.Where(r => r.Categories.Any(c => categoryIds.Contains(c.Id)));

			if(cuisineIds != null && cuisineIds.Count > 0)
				query = query.Where(r => cuisineIds.Contains(r.CuisineId));

			if(difficultyIds != null && difficultyIds.Count > 0)
				query = query.Where(r => difficultyIds.Contains(r.DifficultyId));

			if(productIds != null && productIds.Count > 0)
				query = query.Where(r => r.Ingredients.Any(i => productIds.Contains(i.ProductId)));

			var items = await query
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
				TotalCount = await query.CountAsync()
			};
		}

		public async Task<Recipe?> GetRecipeByIdAsync ( Guid id )
		{
			return await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task<Recipe?> GetRecipeByIdWithAllAsync ( Guid id )
		{
			return await _context.Recipes
				.Include(r => r.Author)
				.Include(r => r.Cuisine)
				.Include(r => r.Difficulty)
				.Include(r => r.Categories)
				.Include(r => r.Ingredients)
					.ThenInclude(i => i.Product)
				.Include(r => r.Ingredients)
					.ThenInclude(i => i.Measure)
				.Include(r => r.Steps)
					.ThenInclude(s => s.SubSteps)
				.Include(r => r.Liked)
				.AsNoTracking()
				.FirstOrDefaultAsync(r => r.Id == id);
		}

		public async Task AddRecipeAsync ( Recipe recipe )
		{
			await _context.Recipes.AddAsync(recipe);
		}

		public void UpdateRecipe ( Recipe recipe )
		{
			_context.Recipes.Update(recipe);
		}

		public void DeleteRecipe ( Recipe recipe )
		{
			_context.Recipes.Remove(recipe);
		}
	}
}