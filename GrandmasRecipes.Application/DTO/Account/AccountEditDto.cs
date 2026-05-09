
namespace GrandmasRecipes.Application.DTO.Account
{
	public class AccountEditDto
	{
		public Guid Id { get; set; }
		public string? Nickname { get; set; }
		public string? Password { get; set; }
		public string? ImageUrl { get; set; }
	}
}
