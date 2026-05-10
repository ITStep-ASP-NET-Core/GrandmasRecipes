using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.WebApi.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[MaxLength(50)]
		public string Nickname { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		[Required]
		[MinLength(8)]
		public string Password { get; set; } = string.Empty;
	}
}
