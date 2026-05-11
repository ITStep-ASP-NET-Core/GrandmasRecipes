using GrandmasRecipes.Application.DTO.Ingredient;
using GrandmasRecipes.Application.DTO.Step;

namespace GrandmasRecipes.Application.DTO.Recipe
{
	public class RecipeCreateDto
	{
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string[]? ImageUrls { get; set; }
		public int Calories { get; set; }
		public int Likes { get; set; }

		public Guid AuthorId { get; set; }
		public int DifficultyId { get; set; }
		public int CuisineId { get; set; }

		public IEnumerable<int>? CategoryIds { get; set; }
		public ICollection<IngredientCreateDto>? Ingredients { get; set; }
		public ICollection<StepCreateDto>? Steps { get; set; }
	}
}
