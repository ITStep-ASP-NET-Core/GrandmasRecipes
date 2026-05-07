using GrandmasRecipes.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
	public class Dificulty
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } = null!;

		public string? ImageUrl { get; set; }

		public DificultyLevel Level { get; set; } = DificultyLevel.None;

		public ICollection<Recipe> Recipes { get; set; } = [];

	}
}
