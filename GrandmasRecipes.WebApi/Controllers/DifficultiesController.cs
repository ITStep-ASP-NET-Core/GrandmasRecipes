using GrandmasRecipes.Application.DTO.Common;
using GrandmasRecipes.Application.DTO.Difficulty;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Управління рівнями складності рецептів.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DifficultiesController : ControllerBase
    {
        private readonly IService<DifficultyDto> _difficultyService;

        public DifficultiesController(IService<DifficultyDto> difficultyService)
        {
            _difficultyService = difficultyService;
        }

        /// <summary>
        /// Отримати всі рівні складності з повною інформацією.
        /// </summary>
        /// <remarks>
        /// Повертає список усіх рівнів складності включно з <c>ImageUrl</c>.
        /// Використовується для сторінок адмін-панелі та каталогу.
        /// </remarks>
        /// <returns>Список <see cref="DifficultyDto"/>.</returns>
        /// <response code="200">Список рівнів складності.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<DifficultyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _difficultyService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Отримати скорочений список рівнів складності.
        /// </summary>
        /// <remarks>
        /// Повертає лише <c>Id</c> та <c>Name</c> без <c>ImageUrl</c>.
        /// Використовується у формах фільтрації та створення рецепту для зменшення трафіку.
        /// </remarks>
        /// <returns>Список <see cref="DifficultySummaryDto"/>.</returns>
        /// <response code="200">Скорочений список рівнів складності.</response>
        [HttpGet("summary")]
        [ProducesResponseType(typeof(ICollection<DifficultySummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSummary()
        {
            var result = await _difficultyService.GetAllAsync();
            var summary = result.Select(x => new DifficultySummaryDto { Id = x.Id, Name = x.Name });
            return Ok(summary);
        }

        /// <summary>
        /// Створити новий рівень складності.
        /// </summary>
        /// <param name="dto">Дані нового рівня складності: назва та опціональне зображення.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="200">Рівень складності успішно створений.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] DifficultyDto dto)
        {
            await _difficultyService.AddAsync(dto);
            return Ok();
        }

        /// <summary>
        /// Видалити рівень складності за ідентифікатором.
        /// </summary>
        /// <remarks>
        /// Якщо рівень складності прив'язаний до рецептів — поведінка залежить від конфігурації БД
        /// (обмеження видалення задане в <c>ApplicationContext.OnModelCreating</c>).
        /// </remarks>
        /// <param name="id">Ідентифікатор рівня складності.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Рівень складності успішно видалений.</response>
        /// <response code="404">Рівень складності з вказаним ідентифікатором не знайдений.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _difficultyService.GetAsync(id);
            if (entity is null)
                return NotFound();

            await _difficultyService.DeleteAsync(entity);
            return NoContent();
        }
    }
}