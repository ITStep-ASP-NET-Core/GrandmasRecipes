using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    /// <summary>
    /// Репозиторій для роботи з акаунтами користувачів.
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>Отримати акаунт за ідентифікатором.</summary>
        /// <param name="id">Ідентифікатор акаунту.</param>
        Task<Account?> GetAccountByIdAsync(Guid id);

        /// <summary>Отримати акаунт за email. Використовується при авторизації.</summary>
        /// <param name="email">Email користувача.</param>
        Task<Account?> GetAccountByEmailAsync(string email);

        /// <summary>Додати новий акаунт.</summary>
        /// <param name="account">Акаунт для додавання.</param>
        Task AddAccountAsync(Account account);

        /// <summary>Оновити дані акаунту.</summary>
        /// <param name="account">Акаунт з оновленими даними.</param>
        void UpdateAccount(Account account);

        /// <summary>Видалити акаунт.</summary>
        /// <param name="account">Акаунт для видалення.</param>
        void DeleteAccount(Account account);
    }
}