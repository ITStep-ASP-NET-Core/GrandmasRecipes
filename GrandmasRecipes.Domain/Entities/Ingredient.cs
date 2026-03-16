using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        public Guid RecipeId { get; set; } // Изменено на Guid, чтобы совпадало с Recipe

        [ForeignKey(nameof(RecipeId))]
        public Recipe? Recipe { get; set; }

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }

        public double Quantity { get; set; }
    }
}