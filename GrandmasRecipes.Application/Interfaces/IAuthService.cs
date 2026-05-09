using GrandmasRecipes.Application.DTO.Auth;

namespace GrandmasRecipes.Application.Interfaces
{
	public interface IAuthService
	{
		Task<AuthResponseDTO> RegisterAsync ( RegisterDto dto );
		Task<AuthResponseDTO> LoginAsync ( LoginDto dto );
		Task<AuthResponseDTO> RefreshAsync ( string refreshToken );
		Task LogoutAsync ( string refreshToken );
	}
}
