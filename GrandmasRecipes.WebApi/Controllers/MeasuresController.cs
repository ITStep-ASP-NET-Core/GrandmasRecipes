using GrandmasRecipes.Application.DTO.Measure;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Управління одиницями виміру для інгредієнтів рецептів.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MeasuresController : ControllerBase
    {
        private readonly IService<MeasureDto> _service;

        public MeasuresController(IService<MeasureDto> service)
        {
            _service = service;
        }

        /// <summary>
        /// Отримати всі одиниці виміру.
        /// </summary>
        /// <returns>Список <see cref="MeasureDto"/>.</returns>
        /// <response code="200">Список одиниць виміру.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<MeasureDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        /// <summary>
        /// Отримати одиницю виміру за ідентифікатором.
        /// </summary>
        /// <param name="id">Ідентифікатор одиниці виміру.</param>
        /// <returns><see cref="MeasureDto"/> при знаходженні.</returns>
        /// <response code="200">Одиниця виміру знайдена.</response>
        /// <response code="404">Одиниця виміру з вказаним ідентифікатором не існує.</response>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MeasureDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Створити нову одиницю виміру.
        /// </summary>
        /// <remarks>
        /// Повертає створений об'єкт та заголовок <c>Location</c> з посиланням на <c>GET /api/measures/{id}</c>.
        /// </remarks>
        /// <param name="dto">Дані нової одиниці виміру.</param>
        /// <returns>Створена одиниця виміру.</returns>
        /// <response code="201">Одиниця виміру успішно створена.</response>
        [HttpPost]
        [ProducesResponseType(typeof(MeasureDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(MeasureDto dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Редагувати одиницю виміру.
        /// </summary>
        /// <param name="dto">Оновлені дані одиниці виміру. <c>Id</c> використовується для ідентифікації запису.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Одиниця виміру успішно оновлена.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Edit(MeasureDto dto)
        {
            await _service.EditAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Видалити одиницю виміру.
        /// </summary>
        /// <param name="dto">DTO одиниці виміру для видалення. <c>Id</c> використовується для ідентифікації запису.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Одиниця виміру успішно видалена.</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(MeasureDto dto)
        {
            await _service.DeleteAsync(dto);
            return NoContent();
        }
    }
}