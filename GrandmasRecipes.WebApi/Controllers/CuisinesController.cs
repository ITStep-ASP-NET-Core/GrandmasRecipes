using GrandmasRecipes.Application.DTO.Common;
using GrandmasRecipes.Application.DTO.Cuisine;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuisinesController : ControllerBase
    {
        private readonly IService<CuisineDto> _cuisineService;

        public CuisinesController(IService<CuisineDto> cuisineService)
        {
            _cuisineService = cuisineService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cuisineService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetAllSummary()
        {
            var result = await _cuisineService.GetAllAsync();
            var summary = result.Select(x => new CuisineSummaryDto { Id = x.Id, Name = x.Name });
            return Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CuisineDto dto)
        {
            await _cuisineService.AddAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
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
