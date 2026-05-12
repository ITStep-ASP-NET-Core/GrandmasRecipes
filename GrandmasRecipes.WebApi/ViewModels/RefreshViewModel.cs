using System.ComponentModel.DataAnnotations;

namespace GrandmasRecipes.WebApi.ViewModels
{
    /// <summary>
    /// Модель запиту для оновлення JWT або виходу з системи.
    /// </summary>
    /// <remarks>
    /// Використовується як вхідні дані для:
    /// <list type="bullet">
    ///   <item><c>POST /api/auth/refresh</c> — оновити JWT без повторного входу.</item>
    ///   <item><c>POST /api/auth/logout</c> — відкликати refresh токен та завершити сесію.</item>
    /// </list>
    /// Refresh токен зберігається в БД і є одноразовим — після використання видається новий.
    /// </remarks>
    public class RefreshViewModel
    {
        /// <summary>
        /// Refresh токен отриманий при вході або попередньому оновленні.
        /// Термін дії — 7 днів.
        /// </summary>
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}