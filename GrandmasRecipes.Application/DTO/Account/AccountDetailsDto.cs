
namespace GrandmasRecipes.Application.DTO.Account
{
	public class AccountDetailsDto
	{
		public Guid Id { get; set; }
		public string? Nickname { get; set; }
		public string? Email { get; set; }
		public string? ImageUrl { get; set; }
		public int Likes { get; set; } = 0;
		public int Published { get; set; } = 0;
		public DateTime? RegisteredAt { get; set; }
		public string? Adress { get; set; }
		public string? Phone { get; set; }
	}
}
