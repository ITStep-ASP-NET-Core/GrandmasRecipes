using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IRecipeRepository
    {
		Task<PagedResult<Recipe>> GetRecipesByLikesAsync ( int page, int pageSize = 10 );
		Task<PagedResult<Recipe>> GetRecipesByAuthorAsync ( Guid authorId, int page, int pageSize = 10 );
		Task<PagedResult<Recipe>> GetRecipesByFiltersAsync (
			ICollection<int>? categoryIds,
			ICollection<int>? cuisineIds,
			ICollection<int>? difficultyIds,
			ICollection<int>? productIds,
			int page,
			int pageSize = 10 );

		Task<Recipe?> GetRecipeByIdWithAllAsync ( Guid id );

        Task AddRecipeAsync ( Recipe recipe );
		void UpdateRecipe ( Recipe recipe );
		void DeleteRecipe ( Recipe recipe );
    }
}