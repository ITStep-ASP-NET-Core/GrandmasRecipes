namespace GrandmasRecipes.Application.DTO.Recipe
{
	/// <summary>
	/// Параметри фільтрації рецептів.
	/// Всі поля опціональні — передавай тільки потрібні фільтри.
	/// Можна комбінувати декілька фільтрів одночасно.
	/// </summary>
	public class RecipeFilterDto
	{
		/// <summary>Пошук за назвою рецепту (регістронезалежний, часткове співпадіння).</summary>
		public string? SearchQuery { get; set; }

		/// <summary>Фільтр за категоріями.</summary>
		public ICollection<int>? CategoryIds { get; set; }

		/// <summary>Фільтр за кухнями.</summary>
		public ICollection<int>? CuisineIds { get; set; }

		/// <summary>Фільтр за рівнями складності.</summary>
		public ICollection<int>? DifficultyIds { get; set; }

		/// <summary>Фільтр за продуктами (рецепти що містять ці продукти).</summary>
		public ICollection<int>? ProductIds { get; set; }
	}
}