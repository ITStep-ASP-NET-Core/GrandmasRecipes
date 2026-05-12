using GrandmasRecipes.Application.DTO.Category;
using GrandmasRecipes.Application.DTO.Common;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Управління категоріями рецептів.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IService<CategoryDto> _categoryService;

        public CategoriesController(IService<CategoryDto> categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Отримати всі категорії з повною інформацією.
        /// </summary>
        /// <remarks>
        /// Повертає список усіх категорій включно з <c>ImageUrl</c>.
        /// Використовується для сторінок адмін-панелі та каталогу.
        /// </remarks>
        /// <returns>Список <see cref="CategoryDto"/>.</returns>
        /// <response code="200">Список категорій.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Отримати скорочений список категорій.
        /// </summary>
        /// <remarks>
        /// Повертає лише <c>Id</c> та <c>Name</c> без <c>ImageUrl</c>.
        /// Використовується у формах фільтрації та створення рецепту для зменшення трафіку.
        /// </remarks>
        /// <returns>Список <see cref="CategorySummaryDto"/>.</returns>
        /// <response code="200">Скорочений список категорій.</response>
        [HttpGet("summary")]
        [ProducesResponseType(typeof(ICollection<CategorySummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSummary()
        {
            var result = await _categoryService.GetAllAsync();
            var summary = result.Select(x => new CategorySummaryDto { Id = x.Id, Name = x.Name });
            return Ok(summary);
        }

        /// <summary>
        /// Створити нову категорію.
        /// </summary>
        /// <param name="dto">Дані нової категорії: назва та опціональне зображення.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="200">Категорія успішно створена.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            await _categoryService.AddAsync(dto);
            return Ok();
        }

        /// <summary>
        /// Видалити категорію за ідентифікатором.
        /// </summary>
        /// <remarks>
        /// Якщо категорія прив'язана до рецептів — поведінка залежить від конфігурації БД
        /// (обмеження видалення задане в <c>ApplicationContext.OnModelCreating</c>).
        /// </remarks>
        /// <param name="id">Ідентифікатор категорії.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Категорія успішно видалена.</response>
        /// <response code="404">Категорія з вказаним ідентифікатором не знайдена.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _categoryService.GetAsync(id);
            if (entity is null)
                return NotFound();

            await _categoryService.DeleteAsync(entity);
            return NoContent();
        }
    }
}