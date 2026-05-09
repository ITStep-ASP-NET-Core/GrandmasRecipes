
namespace GrandmasRecipes.Application.DTO.Ingredient
{
	public class IngredientCreateDto
	{
		public int ProductId { get; set; }
		public int Amount { get; set; }
		public string Measure { get; set; } = string.Empty;
	}
}
