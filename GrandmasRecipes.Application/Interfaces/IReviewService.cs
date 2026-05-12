using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Review;

namespace GrandmasRecipes.Application.Interfaces
{
    /// <summary>
    /// Сервіс для роботи з відгуками на рецепти.
    /// </summary>
    public interface IReviewService
    {
        /// <summary>
        /// Отримати відгуки на рецепт з пагінацією.
        /// Відгуки сортуються від найновіших до найстаріших.
        /// </summary>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        /// <param name="page">Номер сторінки (починається з 0).</param>
        Task<PagedResult<ReviewDto>> GetReviewsAsync(Guid recipeId, int page);

        /// <summary>Додати відгук на рецепт.</summary>
        /// <param name="ReviewDto">Дані для створення відгуку.</param>
        Task<Result> CreateReviewAsync(ReviewCreateDto ReviewDto);
    }
}