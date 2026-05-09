using GrandmasRecipes.Application.DTO.Account;
using GrandmasRecipes.Application.DTO.Category;
using GrandmasRecipes.Application.DTO.Cuisine;
using GrandmasRecipes.Application.DTO.Difficulty;
using GrandmasRecipes.Application.DTO.Ingredient;
using GrandmasRecipes.Application.DTO.Step;

namespace GrandmasRecipes.Application.DTO.Recipe
{
    public class RecipeDetailsDto
    {
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? Description { get; set; }
		public string[]? ImageUrls { get; set; }
		public int Calories { get; set; }
		public int Likes { get; set; }
		public bool IsLiked { get; set; }

		public AccountSummaryDto? Author { get; set; }
		public DifficultySummaryDto? Difficulty { get; set; }
		public CuisineSummaryDto? Cuisine { get; set; }

		public ICollection<CategorySummaryDto>? Categories { get; set; }
		public ICollection<IngredientDto>? Ingredients { get; set; }
		public ICollection<StepDto>? Steps { get; set; }
	}
}