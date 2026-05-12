
namespace GrandmasRecipes.Application.DTO.Review
{
	/// <summary>Дані для створення відгуку на рецепт.</summary>
	public class ReviewCreateDto
	{
		/// <summary>Ідентифікатор рецепту.</summary>
		public Guid RecipeId { get; set; }
		/// <summary>Ідентифікатор автора відгуку.</summary>
		public Guid AuthorId { get; set; }
		/// <summary>Текст відгуку.</summary>
		/// <example>Дуже смачний рецепт!</example>
		public string Comment { get; set; } = string.Empty;
	}
}