using GrandmasRecipes.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
	public class Recipe
	{
		[Key]
		public int Id { get; set; }

		public string Title { get; set; } = null!;

		public int Calories { get; set; }

		public Dificulty Dificulty { get; set; }

		public string? Description { get; set; }

		public string? VideoUrl { get; set; }

		public int? AuthorId { get; set; }

		[ForeignKey(nameof(AuthorId))]
		public Account? Author { get; set; }

		public int? CuisineId { get; set; }

		[ForeignKey(nameof(CuisineId))]
		public Cuisine? Cuisine { get; set; }

		public IEnumerable<string> Photos { get; set; } = [];

		public IEnumerable<int> CategoryIds { get; set; } = [];

		public IEnumerable<Category> Categories { get; set; } = [];

		public IEnumerable<Step> Steps { get; set; } = [];

		public IEnumerable<int> ReviewIds { get; set; } = [];

		public IEnumerable<Review> Reviews { get; set; } = [];

		public IEnumerable<int> IngredientIds { get; set; } = [];

		public IEnumerable<Ingredient> Ingredients { get; set; } = [];

	}
}
