using GrandmasRecipes.Application.DTO.Auth;

namespace GrandmasRecipes.Application.Interfaces
{
    /// <summary>
    /// Сервіс авторизації та автентифікації користувачів.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Зареєструвати нового користувача.
        /// Повертає JWT та refresh токен після успішної реєстрації.
        /// </summary>
        /// <param name="registerDto">Дані для реєстрації.</param>
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

        /// <summary>
        /// Увійти в систему за email та паролем.
        /// Повертає JWT та refresh токен після успішного входу.
        /// </summary>
        /// <param name="loginDto">Дані для входу.</param>
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);

        /// <summary>
        /// Оновити JWT за допомогою refresh токену без повторного входу.
        /// </summary>
        /// <param name="refreshToken">Діючий refresh токен.</param>
        Task<AuthResponseDto> RefreshAsync(string refreshToken);

        /// <summary>
        /// Вийти з системи та відкликати refresh токен.
        /// Після виходу токен більше не може бути використаний.
        /// </summary>
        /// <param name="refreshToken">Refresh токен для відкликання.</param>
        Task LogoutAsync(string refreshToken);
    }
}