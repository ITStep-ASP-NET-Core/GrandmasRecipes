using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string? Description { get; set; }

		public string? ImageUrl { get; set; }

		public ICollection<Recipe> Recipes { get; set; } = [];

	}
}
