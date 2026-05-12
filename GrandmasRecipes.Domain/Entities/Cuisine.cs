using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Кухня народів світу. Наприклад: українська, італійська, японська.
    /// </summary>
    public class Cuisine
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Назва кухні.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Опис кухні.</summary>
        public string? Description { get; set; }

        /// <summary>Посилання на зображення кухні.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Рецепти що належать до цієї кухні.</summary>
        public ICollection<Recipe> Recipes { get; set; } = [];
    }
}