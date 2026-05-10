using GrandmasRecipes.Application.DTO.Auth;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GrandmasRecipes.WebApi.Controllers
{
	[ApiController]
	[Route("api/auth")]
	public class AuthController ( IAuthService authService ) : ControllerBase
	{
		[HttpPost("register")]
		public async Task<IActionResult> Register ( [FromBody] RegisterViewModel model )
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var result = await authService.RegisterAsync(new RegisterDto
				{
					Nickname = model.Nickname,
					Email = model.Email,
					Password = model.Password
				});

				return Ok(result);
			}
			catch(InvalidOperationException ex)
			{
				return Conflict(new { message = ex.Message });
			}
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login ( [FromBody] LoginViewModel model )
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var result = await authService.LoginAsync(new LoginDto
				{
					Email = model.Email,
					Password = model.Password
				});

				return Ok(result);
			}
			catch(UnauthorizedAccessException ex)
			{
				return Unauthorized(new { message = ex.Message });
			}
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh ( [FromBody] RefreshViewModel model )
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var result = await authService.RefreshAsync(model.RefreshToken);
				return Ok(result);
			}
			catch(UnauthorizedAccessException ex)
			{
				return Unauthorized(new { message = ex.Message });
			}
		}

		[HttpPost("logout")]
		public async Task<IActionResult> Logout ( [FromBody] RefreshViewModel model )
		{
			if(!ModelState.IsValid)
				return BadRequest(ModelState);

			await authService.LogoutAsync(model.RefreshToken);
			return NoContent();
		}
	}
}
