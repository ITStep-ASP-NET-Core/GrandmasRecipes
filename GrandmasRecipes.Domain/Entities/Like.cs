using System.ComponentModel.DataAnnotations.Schema;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Лайк користувача на рецепт.
    /// Складений ключ: AccountId + RecipeId.
    /// </summary>
    public class Like
    {
        /// <summary>Ідентифікатор користувача який поставив лайк.</summary>
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account? Account { get; set; }

        /// <summary>Ідентифікатор рецепту який лайкнули.</summary>
        public Guid RecipeId { get; set; }

        [ForeignKey(nameof(RecipeId))]
        public Recipe? Recipe { get; set; }

        /// <summary>Дата та час коли був поставлений лайк.</summary>
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
    }
}