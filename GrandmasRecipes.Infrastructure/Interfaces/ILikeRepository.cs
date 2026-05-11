using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
	public interface ILikeRepository
	{
		Task<ICollection<Guid>> GetLikedRecipeIdsAsync ( Guid accountId );
		Task<Like?> GetLikeAsync ( Guid accountId, Guid recipeId );
		Task<bool> ExistsLikeAsync ( Guid accountId, Guid recipeId );

		Task AddLikeAsync ( Like like );
		void DeleteLike ( Like like );
	}
}
