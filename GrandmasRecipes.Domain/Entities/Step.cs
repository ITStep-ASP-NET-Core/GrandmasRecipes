using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
	public class Step
	{
		[Key]
		public int Id { get; set; }

		public int RecipeId { get; set; }

		public int Name { get; set; }

		public string? Description { get; set; }

		public string? Image { get; set; }

		public IEnumerable<string> Substeps { get; set; } = [];
	}
}
