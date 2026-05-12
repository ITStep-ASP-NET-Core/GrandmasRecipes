using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    /// <summary>
    /// Репозиторій для роботи з лайками на рецепти.
    /// </summary>
    public interface ILikeRepository
    {
        /// <summary>
        /// Отримати ідентифікатори рецептів які лайкнув користувач.
        /// </summary>
        /// <param name="accountId">Ідентифікатор акаунту.</param>
        Task<ICollection<Guid>> GetLikedRecipeIdsAsync(Guid accountId);

        /// <summary>
        /// Отримати лайк за акаунтом та рецептом.
        /// Повертає null якщо лайк не існує.
        /// </summary>
        /// <param name="accountId">Ідентифікатор акаунту.</param>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        Task<Like?> GetLikeAsync(Guid accountId, Guid recipeId);

        /// <summary>
        /// Перевірити чи існує лайк від користувача на рецепт.
        /// </summary>
        /// <param name="accountId">Ідентифікатор акаунту.</param>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        Task<bool> ExistsLikeAsync(Guid accountId, Guid recipeId);

        /// <summary>Додати лайк.</summary>
        /// <param name="like">Лайк для додавання.</param>
        Task AddLikeAsync(Like like);

        /// <summary>Видалити лайк (дизлайк).</summary>
        /// <param name="like">Лайк для видалення.</param>
        void DeleteLike(Like like);
    }
}