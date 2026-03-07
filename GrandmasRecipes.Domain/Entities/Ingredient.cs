using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
	public class Ingredient
	{
		[Key]
		public int Id { get; set; }

		public int RecipeId { get; set; }

		[ForeignKey(nameof(RecipeId))]
		public Recipe? Recipe { get; set; }

		public int ProductId { get; set; }

		[ForeignKey(nameof(ProductId))]
		public Product? Product { get; set; }

		public double Quantity { get; set; }

	}
}
