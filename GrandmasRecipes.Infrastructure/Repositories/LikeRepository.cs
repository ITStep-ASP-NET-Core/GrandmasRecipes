using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
	public class LikeRepository : ILikeRepository
	{
		private readonly ApplicationContext _context;

		public LikeRepository ( ApplicationContext context )
		{
			_context = context;
		}

		public async Task<ICollection<Guid>> GetLikedRecipeIdsAsync ( Guid accountId )
		{
			return await _context.Likes
				.Where(l => l.AccountId == accountId)
				.Select(l => l.RecipeId)
				.ToListAsync();
		}
		public async Task<Like?> GetLikeAsync ( Guid accountId, Guid recipeId )
		{
			return await _context.Likes.FirstOrDefaultAsync(l => l.AccountId == accountId && l.RecipeId == recipeId);
		}

		public async Task<bool> ExistsLikeAsync ( Guid accountId, Guid recipeId )
		{
			return await _context.Likes.AnyAsync(l => l.AccountId == accountId && l.RecipeId == recipeId);
		}

		public async Task AddLikeAsync ( Like like )
		{
			await _context.Likes.AddAsync(like);
		}

		public void DeleteLike ( Like like )
		{
			_context.Likes.Remove(like);
		}
	}
}
