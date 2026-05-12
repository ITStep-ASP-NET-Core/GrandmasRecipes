using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.WebApi.ViewModels
{
    /// <summary>
    /// Модель запиту для входу в систему.
    /// </summary>
    /// <remarks>
    /// Використовується як вхідні дані для <c>POST /api/auth/login</c>.
    /// При успішній автентифікації повертається <see cref="GrandmasRecipes.Application.DTO.Auth.AuthResponseDto"/>
    /// з JWT (15 хв) та refresh токеном (7 днів).
    /// </remarks>
    public class LoginViewModel
    {
        /// <summary>
        /// Email зареєстрованого користувача.
        /// </summary>
        /// <example>olga@example.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Пароль у відкритому вигляді.
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}