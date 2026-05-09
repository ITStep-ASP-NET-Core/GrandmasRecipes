
namespace GrandmasRecipes.Application.DTO.Account
{
	public class AccountDto
	{
		public Guid Id { get; set; }
		public string Nickname { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }
		public int Likes { get; set; } = 0;
		public int Published { get; set; } = 0;
	}
}
