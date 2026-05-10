using GrandmasRecipes.Application.DTO.Common;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IService<LookupDto> _categoryService;

        public CategoriesController(IService<LookupDto> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetAllSummary()
        {
            var result = await _categoryService.GetAllAsync();
            var summary = result.Select(x => new LookupSummaryDto { Id = x.Id, Name = x.Name });
            return Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LookupDto dto)
        {
            await _categoryService.AddAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
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
