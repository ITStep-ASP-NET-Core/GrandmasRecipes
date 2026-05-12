using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    /// <summary>
    /// Репозиторій для роботи з відгуками на рецепти.
    /// </summary>
    public interface IReviewRepository
    {
        /// <summary>
        /// Отримати відгуки на конкретний рецепт з пагінацією.
        /// Відгуки сортуються від найновіших до найстаріших.
        /// </summary>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        /// <param name="page">Номер сторінки (починається з 0).</param>
        /// <param name="pageSize">Кількість відгуків на сторінці.</param>
        Task<PagedResult<Review>> GetReviewsByRecipeIdAsync(Guid recipeId, int page, int pageSize = 10);

        /// <summary>Додати новий відгук.</summary>
        /// <param name="review">Відгук для додавання.</param>
        Task AddReviewAsync(Review review);

        /// <summary>Оновити існуючий відгук.</summary>
        /// <param name="review">Відгук з оновленими даними.</param>
        void UpdateReview(Review review);

        /// <summary>Видалити відгук.</summary>
        /// <param name="review">Відгук для видалення.</param>
        void DeleteReview(Review review);
    }
}