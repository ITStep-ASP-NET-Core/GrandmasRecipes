namespace GrandmasRecipes.Application.DTO.Auth
{
	public class AuthResponseDto
	{
		public string AccessToken { get; set; } = string.Empty;
		public string RefreshToken { get; set; } = string.Empty;
		public string Nickname { get; set; } = string.Empty;
		public Guid AccountId { get; set; }
	}
}
