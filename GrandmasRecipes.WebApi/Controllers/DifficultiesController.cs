using GrandmasRecipes.Application.DTO.Common;
using GrandmasRecipes.Application.DTO.Difficulty;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DifficultiesController : ControllerBase
    {
        private readonly IService<DifficultyDto> _difficultyService;

        public DifficultiesController(IService<DifficultyDto> difficultyService)
        {
            _difficultyService = difficultyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _difficultyService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetAllSummary()
        {
            var result = await _difficultyService.GetAllAsync();
            var summary = result.Select(x => new DifficultySummaryDto { Id = x.Id, Name = x.Name });
            return Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DifficultyDto dto)
        {
            await _difficultyService.AddAsync(dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
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
