using GrandmasRecipes.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
	public class Difficulty
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string? ImageUrl { get; set; }

		public ICollection<Recipe> Recipes { get; set; } = [];

	}
}
