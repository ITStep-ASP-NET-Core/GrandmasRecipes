using GrandmasRecipes.Application.Common;

namespace GrandmasRecipes.Application.Interfaces
{
    /// <summary>
    /// Сервіс для роботи з лайками на рецепти.
    /// </summary>
    public interface ILikeService
    {
        /// <summary>
        /// Перевірити чи лайкнув користувач рецепт.
        /// </summary>
        /// <param name="accountId">Ідентифікатор акаунту.</param>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        Task<bool> IsLikedAsync(Guid accountId, Guid recipeId);

        /// <summary>
        /// Поставити лайк на рецепт.
        /// Повертає помилку якщо лайк вже існує.
        /// </summary>
        /// <param name="accountId">Ідентифікатор акаунту.</param>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        Task<Result> AddLikeAsync(Guid accountId, Guid recipeId);

        /// <summary>
        /// Прибрати лайк з рецепту.
        /// Повертає помилку якщо лайк не існує.
        /// </summary>
        /// <param name="accountId">Ідентифікатор акаунту.</param>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        Task<Result> RemoveLikeAsync(Guid accountId, Guid recipeId);
    }
}