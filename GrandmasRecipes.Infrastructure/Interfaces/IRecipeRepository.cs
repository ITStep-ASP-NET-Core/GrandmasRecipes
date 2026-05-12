using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    /// <summary>
    /// Репозиторій для роботи з рецептами.
    /// Підтримує пагінацію, фільтрацію та отримання повних даних рецепту.
    /// </summary>
    public interface IRecipeRepository
    {
        /// <summary>
        /// Отримати рецепти посортовані за кількістю лайків (від більшого до меншого).
        /// </summary>
        /// <param name="page">Номер сторінки (починається з 0).</param>
        /// <param name="pageSize">Кількість рецептів на сторінці.</param>
        Task<PagedResult<Recipe>> GetRecipesByLikesAsync(int page, int pageSize = 10);

        /// <summary>
        /// Отримати рецепти конкретного автора з пагінацією.
        /// </summary>
        /// <param name="authorId">Ідентифікатор автора.</param>
        /// <param name="page">Номер сторінки (починається з 0).</param>
        /// <param name="pageSize">Кількість рецептів на сторінці.</param>
        Task<PagedResult<Recipe>> GetRecipesByAuthorAsync(Guid authorId, int page, int pageSize = 10);

		/// <summary>
		/// Отримати лайкнуті рецепти конкретного користувача з пагінацією.
		/// </summary>
		/// <param name="userId">Ідентифікатор користувача.</param>
		/// <param name="page">Номер сторінки (починається з 0).</param>
		/// <param name="pageSize">Кількість рецептів на сторінці.</param>
		Task<PagedResult<Recipe>> GetLikedRecipesByUserAsync ( Guid userId, int page, int pageSize = 10 );

		/// <summary>
		/// Отримати рецепти з фільтрацією за категоріями, кухнями, складністю та продуктами.
		/// Всі фільтри опціональні — передавай тільки ті що потрібні.
		/// </summary>
		/// <param name="categoryIds">Ідентифікатори категорій для фільтрації.</param>
		/// <param name="cuisineIds">Ідентифікатори кухонь для фільтрації.</param>
		/// <param name="difficultyIds">Ідентифікатори рівнів складності для фільтрації.</param>
		/// <param name="productIds">Ідентифікатори продуктів для фільтрації.</param>
		/// <param name="page">Номер сторінки (починається з 0).</param>
		/// <param name="pageSize">Кількість рецептів на сторінці.</param>
		Task<PagedResult<Recipe>> GetRecipesByFiltersAsync(
            ICollection<int>? categoryIds,
            ICollection<int>? cuisineIds,
            ICollection<int>? difficultyIds,
            ICollection<int>? productIds,
            int page,
            int pageSize = 10);

        /// <summary>
        /// Отримати повні дані рецепту включаючи автора, інгредієнти, кроки, категорії та лайки.
        /// </summary>
        /// <param name="id">Ідентифікатор рецепту.</param>
        Task<Recipe?> GetRecipeByIdWithAllAsync(Guid id);

        /// <summary>Додати новий рецепт.</summary>
        /// <param name="recipe">Рецепт для додавання.</param>
        Task AddRecipeAsync(Recipe recipe);

        /// <summary>Оновити існуючий рецепт.</summary>
        /// <param name="recipe">Рецепт з оновленими даними.</param>
        void UpdateRecipe(Recipe recipe);

        /// <summary>Видалити рецепт.</summary>
        /// <param name="recipe">Рецепт для видалення.</param>
        void DeleteRecipe(Recipe recipe);
    }
}