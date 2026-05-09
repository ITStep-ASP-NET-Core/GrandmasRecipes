
namespace GrandmasRecipes.Application.DTO.Account
{
	public class AccountSummaryDto
	{
		public Guid Id { get; set; }
		public string Nickname { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }
	}
}
