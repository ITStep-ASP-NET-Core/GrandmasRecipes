namespace GrandmasRecipes.Application.DTO.Account
{
    /// <summary>
    /// Детальна інформація про акаунт.
    /// Використовується в особистому кабінеті та адмін-панелі.
    /// </summary>
    public class AccountDetailsDto
    {
        /// <summary>Ідентифікатор акаунту.</summary>
        public Guid Id { get; set; }

        /// <summary>Нікнейм користувача.</summary>
        /// <example>grandma_olga</example>
        public string? Nickname { get; set; }

        /// <summary>Email користувача.</summary>
        /// <example>olga@example.com</example>
        public string? Email { get; set; }

        /// <summary>Посилання на фото профілю.</summary>
        public string? ImageUrl { get; set; }

        /// <summary>Загальна кількість лайків на всіх рецептах.</summary>
        public int Likes { get; set; }

        /// <summary>Кількість опублікованих рецептів.</summary>
        public int Published { get; set; }

        /// <summary>Дата реєстрації акаунту.</summary>
        public DateTime? RegisteredAt { get; set; }

        /// <summary>Адреса користувача.</summary>
        public string? Adress { get; set; }

        /// <summary>Номер телефону.</summary>
        /// <example>+380991234567</example>
        public string? Phone { get; set; }
    }
}