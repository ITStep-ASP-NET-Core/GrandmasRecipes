using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Відгук користувача на рецепт.
    /// </summary>
    public class Review
    {
        [Key]
        public int Id { get; set; }

        /// <summary>Ідентифікатор рецепту на який залишено відгук.</summary>
        public Guid RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe? Recipe { get; set; }

        /// <summary>Ідентифікатор автора відгуку.</summary>
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        /// <summary>Текст відгуку.</summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>Дата та час надсилання відгуку.</summary>
        public DateTime SendingDate { get; set; } = DateTime.Now;
    }
}