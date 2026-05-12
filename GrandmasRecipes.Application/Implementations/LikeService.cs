using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    /// <summary>
    /// Сервіс управління лайками на рецепти.
    /// </summary>
    public class LikeService : ILikeService
    {
		private readonly IUnitOfWork _uow;

		public LikeService ( IUnitOfWork uow )
		{
			_uow = uow;
		}

		public async Task<bool> IsLikedAsync ( Guid accountId, Guid recipeId )
		{
			return await _uow.Likes.ExistsLikeAsync(accountId, recipeId);
		}

        /// <summary>
        /// Поставити лайк на рецепт.
        /// При додаванні лайку автоматично збільшує лічильник Likes у рецепті.
        /// Повертає помилку якщо лайк вже існує або рецепт не знайдений.
        /// </summary>
        public async Task<Result> AddLikeAsync(Guid accountId, Guid recipeId)
        {
			if(await _uow.Likes.ExistsLikeAsync(accountId, recipeId))
				return Result.Fail("Рецепт уже лайкнут");

			var recipe = await _uow.Recipes.GetRecipeByIdWithAllAsync(recipeId);
			if(recipe is null)
				return Result.Fail("Рецепт не найден");

			await _uow.Likes.AddLikeAsync(new Like { AccountId = accountId, RecipeId = recipeId });
			recipe.Likes++;
			_uow.Recipes.UpdateRecipe(recipe);

			await _uow.SaveChangesAsync();
			return Result.Ok();
		}

        /// <summary>
        /// Прибрати лайк з рецепту.
        /// При видаленні лайку автоматично зменшує лічильник Likes у рецепті (мінімум 0).
        /// Повертає помилку якщо лайк або рецепт не знайдені.
        /// </summary>
        public async Task<Result> RemoveLikeAsync(Guid accountId, Guid recipeId)
        {
			var like = await _uow.Likes.GetLikeAsync(accountId, recipeId);
			if(like is null)
				return Result.Fail("Лайк не найден");

			var recipe = await _uow.Recipes.GetRecipeByIdWithAllAsync(recipeId);
			if(recipe is null)
				return Result.Fail("Рецепт не найден");

			_uow.Likes.DeleteLike(like);
			recipe.Likes = Math.Max(0, recipe.Likes - 1);
			_uow.Recipes.UpdateRecipe(recipe);

			await _uow.SaveChangesAsync();
			return Result.Ok();
		}
	}
}
