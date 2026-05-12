using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
	{
		private readonly ApplicationContext _context;

		public ReviewRepository ( ApplicationContext context )
		{
			_context = context;
		}

		public async Task<PagedResult<Review>> GetReviewsByRecipeIdAsync ( Guid recipeId, int page, int pageSize = 10 )
		{
			var query = _context.Reviews.Where(r => r.RecipeId == recipeId);

			var items = await query
				.OrderByDescending(r => r.SendingDate)
				.Skip(page * pageSize)
				.Take(pageSize)
				.Include(r => r.Account)
				.AsNoTracking()
				.ToListAsync();

			return new PagedResult<Review>
			{
				Items = items,
				PageNumber = page,
				PageSize = pageSize,
				TotalCount = await query.CountAsync()
			};
		}

		public async Task AddReviewAsync ( Review review )
		{
			await _context.Reviews.AddAsync(review);
		}

		public void UpdateReview ( Review review )
		{
			_context.Reviews.Update(review);
		}

		public void DeleteReview ( Review review )
		{
			_context.Reviews.Remove(review);
		}
	}
}