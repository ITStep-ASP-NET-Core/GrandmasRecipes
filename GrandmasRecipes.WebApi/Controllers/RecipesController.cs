using GrandmasRecipes.Application.DTO.Recipe;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> GetRecipe ( Guid id, [FromQuery] Guid? userId = null )
		{
			var result = await _recipeService.GetRecipeByIdAsync(id, userId);
			if(result is null)
				return NotFound();

			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetRecipes ( [FromQuery] int page = 1, [FromQuery] Guid? userId = null )
		{
			var result = await _recipeService.GetRecipesAsync(page, userId);
			return Ok(result);
		}

		[HttpGet("filter")]
		public async Task<IActionResult> GetRecipesByFilters ( [FromQuery] RecipeFilterDto filter, [FromQuery] int page = 1, [FromQuery] Guid? userId = null )
		{
			var result = await _recipeService.GetRecipesByFiltersAsync(filter, page, userId);
			return Ok(result);
		}

		[HttpGet("author/{authorId:guid}")]
		public async Task<IActionResult> GetRecipesByAuthor ( Guid authorId, [FromQuery] int page = 1, [FromQuery] Guid? userId = null )
		{
			var result = await _recipeService.GetRecipesByAuthorAsync(authorId, page, userId);
			return Ok(result);
		}

		[HttpGet("liked/{userId:guid}")]
        public async Task<IActionResult> GetRecipesByLiked(Guid userId, [FromQuery] int page = 1)
        {
            var result = await _recipeService.GetRecipesByLikedAsync(userId, page);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreateDto dto)
        {
            var result = await _recipeService.CreateRecipeAsync(dto);
            if (!result.Success)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> EditRecipe([FromBody] RecipeEditDto dto)
        {
            var result = await _recipeService.EditRecipeAsync(dto);
            if (!result.Success)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var result = await _recipeService.DeleteRecipeAsync(id);
            if (!result.Success)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
