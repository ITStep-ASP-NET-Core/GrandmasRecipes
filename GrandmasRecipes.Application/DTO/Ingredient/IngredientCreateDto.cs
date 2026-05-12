namespace GrandmasRecipes.Application.DTO.Ingredient
{
    /// <summary>Дані для додавання інгредієнту при створенні або редагуванні рецепту.</summary>
    public class IngredientCreateDto
    {
        /// <summary>Ідентифікатор продукту.</summary>
        public int ProductId { get; set; }

        /// <summary>Ідентифікатор одиниці виміру.</summary>
        public int MeasureId { get; set; }

        /// <summary>Кількість продукту.</summary>
        /// <example>2</example>
        public int Amount { get; set; }
    }
}