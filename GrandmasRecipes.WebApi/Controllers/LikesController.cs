using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Управління лайками на рецепти.
    /// </summary>
    /// <remarks>
    /// Маршрут прив'язаний до рецепту: <c>api/recipes/{recipeId}/likes</c>.
    /// При додаванні/видаленні лайку автоматично оновлюється лічильник <c>Likes</c> у рецепті.
    /// Складений ключ лайку: <c>AccountId + RecipeId</c> — один акаунт може лайкнути рецепт лише один раз.
    /// </remarks>
    [ApiController]
    [Route("api/recipes/{recipeId:guid}/likes")]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikesController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        /// <summary>
        /// Перевірити чи лайкнув користувач рецепт.
        /// </summary>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        /// <param name="accountId">Ідентифікатор акаунту.</param>
        /// <returns><c>true</c> якщо лайк існує, <c>false</c> якщо ні.</returns>
        /// <response code="200">Результат перевірки.</response>
        [HttpGet("{accountId:guid}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> IsLiked(Guid recipeId, Guid accountId)
        {
            var result = await _likeService.IsLikedAsync(accountId, recipeId);
            return Ok(result);
        }

        /// <summary>
        /// Поставити лайк на рецепт.
        /// </summary>
        /// <remarks>
        /// Збільшує лічильник <c>Likes</c> у рецепті на 1.
        /// Якщо лайк вже існує — повертає 400 без повторного збільшення лічильника.
        /// </remarks>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        /// <param name="accountId">Ідентифікатор акаунту який ставить лайк.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="200">Лайк успішно додано.</response>
        /// <response code="400">Лайк вже існує або рецепт не знайдений.</response>
        [HttpPost("{accountId:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddLike(Guid recipeId, Guid accountId)
        {
            var result = await _likeService.AddLikeAsync(accountId, recipeId);
            if (!result.Success)
                return BadRequest(result.Error);

            return Ok();
        }

        /// <summary>
        /// Прибрати лайк з рецепту.
        /// </summary>
        /// <remarks>
        /// Зменшує лічильник <c>Likes</c> у рецепті на 1 (мінімум 0).
        /// Якщо лайк не існує або рецепт не знайдений — повертає 400.
        /// </remarks>
        /// <param name="recipeId">Ідентифікатор рецепту.</param>
        /// <param name="accountId">Ідентифікатор акаунту який прибирає лайк.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Лайк успішно видалено.</response>
        /// <response code="400">Лайк не знайдений або рецепт не існує.</response>
        [HttpDelete("{accountId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveLike(Guid recipeId, Guid accountId)
        {
            var result = await _likeService.RemoveLikeAsync(accountId, recipeId);
            if (!result.Success)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}