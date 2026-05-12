using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Review;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Управління відгуками на рецепти.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        /// <summary>
        /// Отримати відгуки на рецепт з пагінацією.
        /// </summary>
        /// <remarks>
        /// Відгуки сортуються від найновіших до найстаріших.
        /// Розмір сторінки визначається на рівні сервісу.
        /// </remarks>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        /// <param name="page">Номер сторінки, починається з 1.</param>
        /// <returns>Пагінований список <see cref="ReviewDto"/>.</returns>
        /// <response code="200">Список відгуків.</response>
        [HttpGet("recipe/{recipeId:guid}")]
        [ProducesResponseType(typeof(PagedResult<ReviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviews(Guid recipeId, [FromQuery] int page = 1)
        {
            var result = await _reviewService.GetReviewsAsync(recipeId, page);
            return Ok(result);
        }

        /// <summary>
        /// Додати відгук на рецепт.
        /// </summary>
        /// <remarks>
        /// Один користувач може залишити кілька відгуків на один рецепт — обмеження не встановлено.
        /// </remarks>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        /// <param name="dto">Дані відгуку: ідентифікатор автора та текст коментаря.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="200">Відгук успішно додано.</response>
        /// <response code="400">Помилка при збереженні відгуку.</response>
        [HttpPost("recipe/{recipeId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReview(Guid recipeId, [FromBody] ReviewCreateDto dto)
        {
            var result = await _reviewService.CreateReviewAsync(dto);
            if (!result.Success)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}