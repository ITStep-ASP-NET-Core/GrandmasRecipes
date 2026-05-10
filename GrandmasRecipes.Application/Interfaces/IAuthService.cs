using GrandmasRecipes.Application.DTO.Auth;

namespace GrandmasRecipes.Application.Interfaces
{
	public interface IAuthService
	{
		Task<AuthResponseDto> RegisterAsync ( RegisterDto registerDto );
		Task<AuthResponseDto> LoginAsync ( LoginDto loginDto );
		Task<AuthResponseDto> RefreshAsync ( string refreshToken );
		Task LogoutAsync ( string refreshToken );
	}
}
