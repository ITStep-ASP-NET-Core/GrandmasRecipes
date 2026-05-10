using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.WebApi.ViewModels
{
	public class RefreshViewModel
	{
		[Required]
		public string RefreshToken { get; set; } = string.Empty;
	}
}
