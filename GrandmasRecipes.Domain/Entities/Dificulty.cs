using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
	public class Dificulty
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string? ImageUrl { get; set; }

		public Enums.Dificulty Level { get; set; } = Enums.Dificulty.None;

		public ICollection<Recipe> Recipes { get; set; } = [];

	}
}
