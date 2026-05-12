using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Складність приготування рецепту.
    /// </summary>
    public class Difficulty
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Назва рівня складності.</summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>Посилання на іконку складності.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Рецепти з цим рівнем складності.</summary>
        public ICollection<Recipe> Recipes { get; set; } = [];
    }
}