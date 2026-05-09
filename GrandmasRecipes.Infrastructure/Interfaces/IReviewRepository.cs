using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IReviewRepository
    {
		Task<PagedResult<Review>> GetReviewsByRecipeIdAsync ( Guid recipeId, int page, int pageSize = 10 );

		Task AddReviewAsync ( Review review );
		void UpdateReview ( Review review );
		void DeleteReview ( Review review );
	}
}