using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Крок приготування рецепту.
    /// Може містити підкроки <see cref="SubStep"/>.
    /// </summary>
    public class Step
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>Порядковий номер кроку.</summary>
        public int Number { get; set; }

        /// <summary>Посилання на зображення кроку.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Заголовок кроку.</summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>Опис кроку приготування.</summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>Ідентифікатор рецепту до якого належить крок.</summary>
        public Guid RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe? Recipe { get; set; }

        /// <summary>Підкроки для деталізації приготування.</summary>
        public ICollection<SubStep>? SubSteps { get; set; }
    }
}