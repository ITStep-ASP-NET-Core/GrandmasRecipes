namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Звичайний користувач системи.
    /// Розширює <see cref="Account"/> додатковими полями профілю.
    /// </summary>
    public class User : Account
    {
        /// <summary>Посилання на фото профілю.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Загальна кількість лайків на всіх рецептах користувача.</summary>
        public int Likes { get; set; } = 0;

        /// <summary>Кількість опублікованих рецептів.</summary>
        public int Published { get; set; } = 0;

        /// <summary>Дата реєстрації акаунту.</summary>
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}