namespace GrandmasRecipes.Application.DTO.Account
{
    /// <summary>
    /// Дані для редагування акаунту.
    /// Всі поля окрім Id опціональні — передавай тільки те що змінюєш.
    /// </summary>
    public class AccountEditDto
    {
        /// <summary>Ідентифікатор акаунту для редагування.</summary>
        public Guid Id { get; set; }

        /// <summary>Новий нікнейм.</summary>
        /// <example>grandma_olga_new</example>
        public string? Nickname { get; set; }

        /// <summary>Новий пароль у відкритому вигляді.</summary>
        public string? Password { get; set; }

        /// <summary>Нове посилання на фото профілю.</summary>
        public string? ImageUrl { get; set; }
    }
}