
namespace GrandmasRecipes.Application.DTO.Recipe
{
	public class RecipePreviewDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string? ImageUrl { get; set; }
		public int Likes { get; set; }
		public bool IsLiked { get; set; }
		public string? AuthorNickname { get; set; }
	}
}
