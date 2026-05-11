
namespace GrandmasRecipes.Application.DTO.Ingredient
{
	public class IngredientDto
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public int MeasureId { get; set; }
		public string Measure { get; set; } = string.Empty;
		public int Amount { get; set; }
	}
}
