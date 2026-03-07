using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
	public class Account
	{
		[Key]
		public int Id { get; set; }

		public string Email { get; set; } = null!;

		public string PasswordHash { get; set; } = null!;

	}
}