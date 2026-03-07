using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
	public class Cuisine
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string? Description { get; set; }

		public IEnumerable<Recipe> Recipes { get; set; } = [];

	}
}
