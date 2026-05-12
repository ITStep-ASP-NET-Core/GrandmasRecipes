using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    /// <summary>
    /// Репозиторій для роботи з refresh токенами JWT авторизації.
    /// </summary>
    public interface IRefreshTokenRepository
    {
        /// <summary>
        /// Отримати refresh токен за його значенням.
        /// Повертає null якщо токен не знайдений або відкликаний.
        /// </summary>
        /// <param name="token">Значення токену.</param>
        Task<RefreshToken?> GetByTokenAsync(string token);

        /// <summary>Зберегти новий refresh токен.</summary>
        /// <param name="refreshToken">Токен для збереження.</param>
        Task CreateAsync(RefreshToken refreshToken);

        /// <summary>
        /// Відкликати refresh токен.
        /// Після відкликання токен більше не може бути використаний.
        /// </summary>
        /// <param name="refreshToken">Токен для відкликання.</param>
        Task RevokeAsync(RefreshToken refreshToken);
    }
}