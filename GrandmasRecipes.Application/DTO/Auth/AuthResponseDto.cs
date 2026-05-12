namespace GrandmasRecipes.Application.DTO.Auth
{
    /// <summary>
    /// Відповідь після успішної авторизації або оновлення токену.
    /// </summary>
    public class AuthResponseDto
    {
        /// <summary>JWT токен для авторизації запитів. Діє 15 хвилин.</summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>Refresh токен для оновлення JWT без повторного входу. Діє 7 днів.</summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>Нікнейм авторизованого користувача.</summary>
        public string Nickname { get; set; } = string.Empty;

        /// <summary>Ідентифікатор авторизованого акаунту.</summary>
        public Guid AccountId { get; set; }
    }
}