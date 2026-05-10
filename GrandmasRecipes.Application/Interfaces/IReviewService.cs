using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Review;

namespace GrandmasRecipes.Application.Interfaces
{
    public interface IReviewService
	{
        Task<PagedResult<ReviewDto>> GetReviewsAsync( Guid recipeId, int page );

		Task<Result> CreateReviewAsync ( ReviewCreateDto ReviewDto );
	}
}