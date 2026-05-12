using GrandmasRecipes.Application.DTO.Common;
using GrandmasRecipes.Application.DTO.Product;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    /// <summary>
    /// Управління продуктами для інгредієнтів рецептів.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IService<ProductDto> _productService;

        public ProductsController(IService<ProductDto> productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Отримати всі продукти з повною інформацією.
        /// </summary>
        /// <remarks>
        /// Повертає список усіх продуктів включно з <c>ImageUrl</c>.
        /// Використовується для сторінок адмін-панелі.
        /// </remarks>
        /// <returns>Список <see cref="ProductDto"/>.</returns>
        /// <response code="200">Список продуктів.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ICollection<ProductDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Отримати скорочений список продуктів.
        /// </summary>
        /// <remarks>
        /// Повертає лише <c>Id</c> та <c>Name</c> без <c>ImageUrl</c>.
        /// Використовується у формі додавання інгредієнтів при створенні рецепту.
        /// </remarks>
        /// <returns>Список <see cref="ProductSummaryDto"/>.</returns>
        /// <response code="200">Скорочений список продуктів.</response>
        [HttpGet("summary")]
        [ProducesResponseType(typeof(ICollection<ProductSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSummary()
        {
            var result = await _productService.GetAllAsync();
            var summary = result.Select(x => new ProductSummaryDto { Id = x.Id, Name = x.Name });
            return Ok(summary);
        }

        /// <summary>
        /// Створити новий продукт.
        /// </summary>
        /// <param name="dto">Дані нового продукту: назва та опціональне зображення.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="200">Продукт успішно створений.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            await _productService.AddAsync(dto);
            return Ok();
        }

        /// <summary>
        /// Видалити продукт за ідентифікатором.
        /// </summary>
        /// <remarks>
        /// Якщо продукт використовується в інгредієнтах рецептів — поведінка залежить від конфігурації БД.
        /// </remarks>
        /// <param name="id">Ідентифікатор продукту.</param>
        /// <returns>Порожня відповідь при успіху.</returns>
        /// <response code="204">Продукт успішно видалений.</response>
        /// <response code="404">Продукт з вказаним ідентифікатором не знайдений.</response>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _productService.GetAsync(id);
            if (entity is null)
                return NotFound();

            await _productService.DeleteAsync(entity);
            return NoContent();
        }
    }
}