using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
	public class Like
	{
		public Guid AccountId { get; set; }

		[ForeignKey(nameof(AccountId))]
		public Account? Account { get; set; }

		public Guid RecipeId { get; set; }

		[ForeignKey(nameof(RecipeId))]
		public Recipe? Recipe { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
    }
}