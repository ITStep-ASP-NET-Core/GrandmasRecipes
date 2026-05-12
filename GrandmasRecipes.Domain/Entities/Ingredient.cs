using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Інгредієнт рецепту.
    /// Пов'язує продукт з рецептом та вказує кількість і одиницю виміру.
    /// </summary>
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Ідентифікатор рецепту до якого належить інгредієнт.</summary>
        public Guid RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe? Recipe { get; set; }

        /// <summary>Ідентифікатор продукту.</summary>
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        /// <summary>Ідентифікатор одиниці виміру.</summary>
        public int MeasureId { get; set; }

        [ForeignKey(nameof(MeasureId))]
        public Measure? Measure { get; set; }

        /// <summary>Кількість продукту у вказаній одиниці виміру.</summary>
        public double Quantity { get; set; }
    }
}