using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Категорія рецептів. Наприклад: супи, десерти, салати.
    /// </summary>
    public class Category
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Назва категорії.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Опис категорії.</summary>
        public string? Description { get; set; }

        /// <summary>Посилання на зображення категорії.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Рецепти що належать до цієї категорії.</summary>
        public ICollection<Recipe> Recipes { get; set; } = [];
    }
}