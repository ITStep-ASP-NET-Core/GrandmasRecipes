using GrandmasRecipes.Application.DTO.Common;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuisinesController : ControllerBase
    {
        private readonly IService<LookupDto> _cuisineService;

        public CuisinesController(IService<LookupDto> cuisineService)
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
            var summary = result.Select(x => new LookupSummaryDto { Id = x.Id, Name = x.Name });
            return Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LookupDto dto)
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
