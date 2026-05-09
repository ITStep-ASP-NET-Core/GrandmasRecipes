using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
	public class RefreshToken
	{
		[Key]
		public int Id { get; set; }

		public string Token { get; set; } = string.Empty;

		public DateTime ExpiresAt { get; set; }

		public bool IsRevoked { get; set; } = false;

		public Guid AccountId { get; set; }

		[ForeignKey(nameof(AccountId))]
		public Account? Account { get; set; }
	}
}
