using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Recipe;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Управління рецептами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        /// <summary>
        /// Отримати рецепт за ідентифікатором.
        /// </summary>
        /// <remarks>
        /// Повертає повну інформацію: автора, складність, кухню, категорії, інгредієнти та кроки.
        /// Якщо передати <paramref name="userId"/>, поле <c>IsLiked</c> буде заповнено відповідно до лайків цього користувача.
        /// </remarks>
        /// <param name="id">Ідентифікатор рецепту.</param>
        /// <param name="userId">Опціональний ідентифікатор поточного користувача для визначення <c>IsLiked</c>.</param>
        /// <returns>Детальна інформація про рецепт <see cref="RecipeDetailsDto"/>.</returns>
        /// <response code="200">Рецепт знайдений.</response>
        /// <response code="404">Рецепт з вказаним ідентифікатором не існує.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RecipeDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecipe(Guid id, [FromQuery] Guid? userId = null)
        {
            var result = await _recipeService.GetRecipeByIdAsync(id, userId);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Отримати список рецептів з пагінацією, посортованих за популярністю.
        /// </summary>
        /// <remarks>
        /// Рецепти сортуються за кількістю лайків (спадання).
        /// Розмір сторінки визначається на рівні сервісу.
        /// Якщо передати <paramref name="userId"/>, поле <c>IsLiked</c> заповнюється для кожного рецепту
        /// одним запитом до БД замість перевірки кожного рецепту окремо.
        /// </remarks>
        /// <param name="page">Номер сторінки, починається з 1.</param>
        /// <param name="userId">Опціональний ідентифікатор поточного користувача для визначення <c>IsLiked</c>.</param>
        /// <returns>Пагінований список <see cref="RecipePreviewDto"/>.</returns>
        /// <response code="200">Список рецептів.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<RecipePreviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecipes([FromQuery] int page = 1, [FromQuery] Guid? userId = null)
        {
            var result = await _recipeService.GetRecipesAsync(page, userId);
            return Ok(result);
        }

        /// <summary>
        /// Отримати рецепти з фільтрацією.
        /// </summary>
        /// <remarks>
        /// Всі параметри фільтра опціональні та можуть комбінуватись між собою.
        /// Підтримується фільтрація за категоріями, кухнями, рівнями складності та продуктами.
        /// Результат посортований за кількістю лайків (спадання).
        /// </remarks>
        /// <param name="filter">Параметри фільтрації. Дивись <see cref="RecipeFilterDto"/>.</param>
        /// <param name="page">Номер сторінки, починається з 1.</param>
        /// <param name="userId">Опціональний ідентифікатор поточного користувача для визначення <c>IsLiked</c>.</param>
        /// <returns>Пагінований відфільтрований список <see cref="RecipePreviewDto"/>.</returns>
        /// <response code="200">Відфільтрований список рецептів.</response>
        [HttpGet("filter")]
        [ProducesResponseType(typeof(PagedResult<RecipePreviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecipesByFilters([FromQuery] RecipeFilterDto filter, [FromQuery] int page = 1, [FromQuery] Guid? userId = null)
        {
            var result = await _recipeService.GetRecipesByFiltersAsync(filter, page, userId);
            return Ok(result);
        }

        /// <summary>
        /// Отримати рецепти конкретного автора.
        /// </summary>
        /// <param name="authorId">Ідентифікатор автора.</param>
        /// <param name="page">Номер сторінки, починається з 1.</param>
        /// <param name="userId">Опціональний ідентифікатор поточного користувача для визначення <c>IsLiked</c>.</param>
        /// <returns>Пагінований список рецептів автора <see cref="RecipePreviewDto"/>.</returns>
        /// <response code="200">Список рецептів автора.</response>
        [HttpGet("author/{authorId:guid}")]
        [ProducesResponseType(typeof(PagedResult<RecipePreviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecipesByAuthor(Guid authorId, [FromQuery] int page = 1, [FromQuery] Guid? userId = null)
        {
            var result = await _recipeService.GetRecipesByAuthorAsync(authorId, page, userId);
            return Ok(result);
        }

        /// <summary>
        /// Отримати рецепти які лайкнув користувач.
        /// </summary>
        /// <remarks>
        /// Рецепти сортуються за датою лайку (спадання) — найновіший лайк іде першим.
        /// </remarks>
        /// <param name="userId">Ідентифікатор користувача.</param>
        /// <param name="page">Номер сторінки, починається з 1.</param>
        /// <returns>Пагінований список лайкнутих рецептів <see cref="RecipePreviewDto"/>.</returns>
        /// <response code="200">Список лайкнутих рецептів.</response>
        [HttpGet("liked/{userId:guid}")]
        [ProducesResponseType(typeof(PagedResult<RecipePreviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecipesByLiked(Guid userId, [FromQuery] int page = 1)
        {
            var result = await _recipeService.GetRecipesByLikedAsync(userId, page);
            return Ok(result);
        }

        /// <summary>
        /// Створити новий рецепт.
        /// </summary>
        /// <remarks>
        /// Рецепт прив'язується до автора через <c>AuthorId</c> з тіла запиту.
        /// Інгредієнти та кроки зберігаються разом з рецептом в одній транзакції.
        /// </remarks>
        /// <param name="dto">Дані нового рецепту. Дивись <see cref="RecipeCreateDto"/>.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="200">Рецепт успішно створений.</response>
        /// <response code="400">Помилка валідації або збереження даних.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreateDto dto)
        {
            var result = await _recipeService.CreateRecipeAsync(dto);
            if (!result.Success)
                return BadRequest(result.Error);

            return Ok();
        }

        /// <summary>
        /// Редагувати рецепт.
        /// </summary>
        /// <remarks>
        /// Часткове оновлення — передавай тільки поля що змінюються, решта залишається без змін.
        /// Якщо передані нові інгредієнти або кроки — старі повністю замінюються новими.
        /// </remarks>
        /// <param name="dto">Дані для оновлення. Обов'язковий лише <c>RecipeId</c>. Дивись <see cref="RecipeEditDto"/>.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Рецепт успішно оновлений.</response>
        /// <response code="400">Рецепт з вказаним <c>RecipeId</c> не знайдений або помилка валідації.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditRecipe([FromBody] RecipeEditDto dto)
        {
            var result = await _recipeService.EditRecipeAsync(dto);
            if (!result.Success)
                return BadRequest(result.Error);

            return NoContent();
        }

        /// <summary>
        /// Видалити рецепт.
        /// </summary>
        /// <remarks>
        /// Каскадне видалення: разом з рецептом видаляються всі інгредієнти, кроки та відгуки.
        /// Лічильники лайків та публікацій у акаунту автора оновлюються автоматично.
        /// Операція незворотня.
        /// </remarks>
        /// <param name="id">Ідентифікатор рецепту для видалення.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Рецепт та всі пов'язані дані успішно видалені.</response>
        /// <response code="400">Рецепт з вказаним ідентифікатором не знайдений.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var result = await _recipeService.DeleteRecipeAsync(id);
            if (!result.Success)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}