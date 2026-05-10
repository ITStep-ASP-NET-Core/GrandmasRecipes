using GrandmasRecipes.Application.DTO.Review;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReviewsController : ControllerBase
	{
		private readonly IReviewService _reviewService;

		public ReviewsController ( IReviewService reviewService )
		{
			_reviewService = reviewService;
		}

		[HttpGet("recipe/{recipeId:guid}")]
		public async Task<IActionResult> GetReviews ( Guid recipeId, [FromQuery] int page = 1 )
		{
			var result = await _reviewService.GetReviewsAsync(recipeId, page);
			return Ok(result);
		}

		[HttpPost("recipe/{recipeId:guid}")]
		public async Task<IActionResult> CreateReview ( Guid recipeId, [FromBody] ReviewCreateDto dto )
		{
			var result = await _reviewService.CreateReviewAsync(dto);
			if(!result.Success)
				return BadRequest(result.Error);

			return Ok();
		}
	}
}