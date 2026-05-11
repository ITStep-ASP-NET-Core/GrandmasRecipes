using GrandmasRecipes.Application.Common;

namespace GrandmasRecipes.Application.Interfaces
{
	public interface ILikeService
	{
		Task<bool> IsLikedAsync ( Guid accountId, Guid recipeId );
		Task<Result> AddLikeAsync ( Guid accountId, Guid recipeId );
		Task<Result> RemoveLikeAsync ( Guid accountId, Guid recipeId );
	}
}
