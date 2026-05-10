using GrandmasRecipes.Application.DTO.Common;
using GrandmasRecipes.Application.DTO.Product;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IService<ProductDto> _productService;

        public ProductsController(IService<ProductDto> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetAllSummary()
        {
            var result = await _productService.GetAllAsync();
            var summary = result.Select(x => new ProductSummaryDto { Id = x.Id, Name = x.Name });
            return Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            await _productService.AddAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
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
