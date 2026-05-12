namespace GrandmasRecipes.Application.DTO.Auth
{
    /// <summary>Дані для реєстрації нового користувача.</summary>
    public class RegisterDto
    {
        /// <summary>Нікнейм нового користувача.</summary>
        /// <example>grandma_olga</example>
        public string Nickname { get; set; } = string.Empty;

        /// <summary>Email нового користувача. Повинен бути унікальним.</summary>
        /// <example>olga@example.com</example>
        public string Email { get; set; } = string.Empty;

        /// <summary>Пароль у відкритому вигляді. Буде захешований перед збереженням.</summary>
        public string Password { get; set; } = string.Empty;
    }
}