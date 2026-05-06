using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public Guid RecipeId { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public Recipe? Recipe { get; set; }

        public Guid AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        public string Comment { get; set; } = string.Empty;
    }
}