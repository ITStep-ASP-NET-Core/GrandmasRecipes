using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    [Table("Steps")]
    public class Step
    {
        [Key]
        public Guid Id { get; set; } // Свой собственный Guid

        public int Number { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        // Связь с рецептом (Guid совпадает с Guid в Recipe)
        public Guid RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; } = null!;
    }
}