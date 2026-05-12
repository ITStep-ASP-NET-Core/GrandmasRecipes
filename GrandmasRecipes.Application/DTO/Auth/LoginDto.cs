namespace GrandmasRecipes.Application.DTO.Auth
{
    /// <summary>Дані для входу в систему.</summary>
    public class LoginDto
    {
        /// <summary>Email користувача.</summary>
        /// <example>olga@example.com</example>
        public string Email { get; set; } = string.Empty;

        /// <summary>Пароль у відкритому вигляді.</summary>
        public string Password { get; set; } = string.Empty;
    }
}