namespace GrandmasRecipes.Application.DTO.Account
{
    /// <summary>
    /// Скорочена інформація про акаунт.
    /// Використовується у RecipeDetailsDto як автор рецепту.
    /// </summary>
    public class AccountSummaryDto
    {
        /// <summary>Ідентифікатор акаунту.</summary>
        public Guid Id { get; set; }

        /// <summary>Нікнейм користувача.</summary>
        public string Nickname { get; set; } = string.Empty;

        /// <summary>Посилання на фото профілю.</summary>
        public string? ImageUrl { get; set; }
    }
}