using GrandmasRecipes.Application.DTO.Common;
using GrandmasRecipes.Application.DTO.Cuisine;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Управління кухнями народів світу.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CuisinesController : ControllerBase
    {
        private readonly IService<CuisineDto> _cuisineService;

        public CuisinesController(IService<CuisineDto> cuisineService)
        {
            _cuisineService = cuisineService;
        }

        /// <summary>
        /// Отримати всі кухні з повною інформацією.
        /// </summary>
        /// <remarks>
        /// Повертає список усіх кухонь включно з <c>ImageUrl</c>.
        /// Використовується для сторінок адмін-панелі та каталогу.
        /// </remarks>
        /// <returns>Список <see cref="CuisineDto"/>.</returns>
        /// <response code="200">Список кухонь.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<CuisineDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cuisineService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Отримати скорочений список кухонь.
        /// </summary>
        /// <remarks>
        /// Повертає лише <c>Id</c> та <c>Name</c> без <c>ImageUrl</c>.
        /// Використовується у формах фільтрації та створення рецепту для зменшення трафіку.
        /// </remarks>
        /// <returns>Список <see cref="CuisineSummaryDto"/>.</returns>
        /// <response code="200">Скорочений список кухонь.</response>
        [HttpGet("summary")]
        [ProducesResponseType(typeof(ICollection<CuisineSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSummary()
        {
            var result = await _cuisineService.GetAllAsync();
            var summary = result.Select(x => new CuisineSummaryDto { Id = x.Id, Name = x.Name });
            return Ok(summary);
        }

        /// <summary>
        /// Створити нову кухню.
        /// </summary>
        /// <param name="dto">Дані нової кухні: назва та опціональне зображення.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="200">Кухня успішно створена.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CuisineDto dto)
        {
            await _cuisineService.AddAsync(dto);
            return Ok();
        }

        /// <summary>
        /// Видалити кухню за ідентифікатором.
        /// </summary>
        /// <remarks>
        /// Якщо кухня прив'язана до рецептів — поведінка залежить від конфігурації БД
        /// (обмеження видалення задане в <c>ApplicationContext.OnModelCreating</c>).
        /// </remarks>
        /// <param name="id">Ідентифікатор кухні.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Кухня успішно видалена.</response>
        /// <response code="404">Кухня з вказаним ідентифікатором не знайдена.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _cuisineService.GetAsync(id);
            if (entity is null)
                return NotFound();

            await _cuisineService.DeleteAsync(entity);
            return NoContent();
        }
    }
}