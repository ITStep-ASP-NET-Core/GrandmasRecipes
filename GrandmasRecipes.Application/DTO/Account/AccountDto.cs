namespace GrandmasRecipes.Application.DTO.Account
{
    /// <summary>
    /// Публічна інформація про акаунт.
    /// Використовується у профілі користувача.
    /// </summary>
    public class AccountDto
    {
        /// <summary>Ідентифікатор акаунту.</summary>
        public Guid Id { get; set; }

        /// <summary>Нікнейм користувача.</summary>
        /// <example>grandma_olga</example>
        public string Nickname { get; set; } = string.Empty;

        /// <summary>Посилання на фото профілю.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Загальна кількість лайків на всіх рецептах.</summary>
        /// <example>342</example>
        public int Likes { get; set; }

        /// <summary>Кількість опублікованих рецептів.</summary>
        /// <example>12</example>
        public int Published { get; set; }
    }
}