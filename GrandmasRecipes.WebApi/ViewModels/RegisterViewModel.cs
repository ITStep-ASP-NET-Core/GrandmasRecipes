using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.WebApi.ViewModels
{
    /// <summary>
    /// Модель запиту для реєстрації нового користувача.
    /// </summary>
    /// <remarks>
    /// Використовується як вхідні дані для <c>POST /api/auth/register</c>.
    /// Всі поля обов'язкові та проходять валідацію через DataAnnotations.
    /// Після успішної реєстрації повертається <see cref="GrandmasRecipes.Application.DTO.Auth.AuthResponseDto"/>.
    /// </remarks>
    public class RegisterViewModel
    {
        /// <summary>
        /// Нікнейм нового користувача. Максимум 50 символів.
        /// </summary>
        /// <example>grandma_olga</example>
        [Required]
        [MaxLength(50)]
        public string Nickname { get; set; } = string.Empty;

        /// <summary>
        /// Email нового користувача. Повинен бути унікальним у системі.
        /// </summary>
        /// <example>olga@example.com</example>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Пароль у відкритому вигляді. Мінімум 8 символів.
        /// Буде захешований через Argon2id перед збереженням.
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }
}