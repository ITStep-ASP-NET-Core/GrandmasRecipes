using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
	[ApiController]
	[Route("api/recipes/{recipeId:guid}/likes")]
	public class LikesController : ControllerBase
	{
		private readonly ILikeService _likeService;

		public LikesController ( ILikeService likeService )
		{
			_likeService = likeService;
		}

		[HttpGet("{accountId:guid}")]
		public async Task<IActionResult> IsLiked ( Guid recipeId, Guid accountId )
		{
			var result = await _likeService.IsLikedAsync(accountId, recipeId);
			return Ok(result);
		}

		[HttpPost("{accountId:guid}")]
		public async Task<IActionResult> AddLike ( Guid recipeId, Guid accountId )
		{
			var result = await _likeService.AddLikeAsync(accountId, recipeId);
			if(!result.Success)
				return BadRequest(result.Error);

			return Ok();
		}

		[HttpDelete("{accountId:guid}")]
		public async Task<IActionResult> RemoveLike ( Guid recipeId, Guid accountId )
		{
			var result = await _likeService.RemoveLikeAsync(accountId, recipeId);
			if(!result.Success)
				return BadRequest(result.Error);

			return NoContent();
		}
	}
}
