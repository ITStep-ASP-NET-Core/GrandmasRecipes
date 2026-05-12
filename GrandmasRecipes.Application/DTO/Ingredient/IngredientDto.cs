namespace GrandmasRecipes.Application.DTO.Ingredient
{
    /// <summary>Інгредієнт рецепту з повною інформацією про продукт та одиницю виміру.</summary>
    public class IngredientDto
    {
        /// <summary>Ідентифікатор продукту.</summary>
        public int ProductId { get; set; }

        /// <summary>Назва продукту.</summary>
        /// <example>Буряк</example>
        public string ProductName { get; set; } = string.Empty;

        /// <summary>Ідентифікатор одиниці виміру.</summary>
        public int MeasureId { get; set; }

        /// <summary>Назва одиниці виміру.</summary>
        /// <example>кг</example>
        public string Measure { get; set; } = string.Empty;

        /// <summary>Кількість продукту.</summary>
        /// <example>2</example>
        public int Amount { get; set; }
    }
}