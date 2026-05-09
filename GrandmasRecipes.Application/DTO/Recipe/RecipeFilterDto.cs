namespace GrandmasRecipes.Application.DTO.Recipe
{
	public class RecipeFilterDto
	{
		public ICollection<int>? CategoryIds { get; set; }
		public ICollection<int>? CuisineIds { get; set; }
		public ICollection<int>? DificultyIds { get; set; }
		public ICollection<int>? ProductIds { get; set; }
	}
}
