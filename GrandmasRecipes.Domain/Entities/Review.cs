using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
	public class Review
	{
		[Key]
		public int Id { get; set; }

		public int RecipeId { get; set; }

		[ForeignKey(nameof(RecipeId))]
		public Recipe? Recipes { get; set; }

		public int AccountId { get; set; }

		[ForeignKey(nameof(AccountId))]
		public Account? Account { get; set; }

		public int Rating { get; set; }

		public string? Text { get; set; }

	}
}
