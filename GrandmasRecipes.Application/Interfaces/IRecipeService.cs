using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Recipe;

namespace GrandmasRecipes.Application.Interfaces
{
    public interface IRecipeService
    {
        Task<PagedResult<RecipePreviewDto>> GetRecipesAsync( int page );
		Task<PagedResult<RecipePreviewDto>> GetRecipesByFiltersAsync ( RecipeFilterDto filter, int page );
        Task<PagedResult<RecipePreviewDto>> GetRecipesByAuthorAsync( Guid authorId, int page );
        Task<PagedResult<RecipePreviewDto>> GetRecipesByLikedAsync( Guid userId, int page );
		Task<RecipeDetailsDto?> GetRecipeByIdAsync( Guid recipeId );

		Task<Result> CreateRecipeAsync ( RecipeCreateDto recipeDto );
		Task<Result> EditRecipeAsync ( RecipeEditDto recipeDto );
		Task<Result> DeleteRecipeAsync ( Guid recipeId );
	}
}