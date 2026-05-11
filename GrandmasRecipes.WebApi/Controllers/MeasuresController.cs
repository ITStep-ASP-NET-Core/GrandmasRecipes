using GrandmasRecipes.Application.DTO.Measure;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MeasuresController : ControllerBase
	{
		private readonly IService<MeasureDto> _service;

		public MeasuresController ( IService<MeasureDto> service )
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll ( ) =>
			Ok(await _service.GetAllAsync());

		[HttpGet("{id:int}")]
		public async Task<IActionResult> Get ( int id )
		{
			var result = await _service.GetAsync(id);
			return result is null ? NotFound() : Ok(result);
		}

		[HttpPost]
		public async Task<IActionResult> Create ( MeasureDto dto )
		{
			await _service.AddAsync(dto);
			return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
		}

		[HttpPut]
		public async Task<IActionResult> Edit ( MeasureDto dto )
		{
			await _service.EditAsync(dto);
			return NoContent();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete ( MeasureDto dto )
		{
			await _service.DeleteAsync(dto);
			return NoContent();
		}
	}
}