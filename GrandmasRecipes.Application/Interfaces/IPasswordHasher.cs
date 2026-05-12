namespace GrandmasRecipes.Application.Interfaces
{
    /// <summary>
    /// Сервіс для хешування та перевірки паролів.
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Захешувати пароль перед збереженням в базу даних.
        /// </summary>
        /// <param name="password">Пароль у відкритому вигляді.</param>
        /// <returns>Хеш пароля.</returns>
        string HashPassword(string password);

        /// <summary>
        /// Перевірити чи відповідає пароль збереженому хешу.
        /// </summary>
        /// <param name="password">Пароль у відкритому вигляді.</param>
        /// <param name="hashedPassword">Збережений хеш пароля.</param>
        /// <returns>True якщо пароль вірний, false якщо ні.</returns>
        bool VerifyPassword(string password, string hashedPassword);
    }
}