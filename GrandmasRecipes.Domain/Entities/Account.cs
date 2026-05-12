using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.Domain.Entities
{
    /// <summary>
    /// Базовий акаунт користувача системи.
    /// Містить дані авторизації, рецепти, лайки та відгуки.
    /// </summary>
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>Нікнейм користувача.</summary>
        public string Nickname { get; set; } = string.Empty;

        /// <summary>Email користувача. Використовується для входу.</summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>Хеш пароля. Ніколи не повертається в API.</summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>Refresh токени для JWT авторизації.</summary>
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

        /// <summary>Рецепти опубліковані цим акаунтом.</summary>
        public ICollection<Recipe> Recipes { get; set; } = [];

        /// <summary>Рецепти які лайкнув цей акаунт.</summary>
        public ICollection<Like> Liked { get; set; } = [];

        /// <summary>Відгуки залишені цим акаунтом.</summary>
        public ICollection<Review> Reviews { get; set; } = [];
    }
}