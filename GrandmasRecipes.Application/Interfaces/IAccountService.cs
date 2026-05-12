using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Account;

namespace GrandmasRecipes.Application.Interfaces
{
    /// <summary>
    /// Сервіс для роботи з акаунтами користувачів.
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Отримати публічну інформацію про акаунт.
        /// </summary>
        /// <param name="accountId">Ідентифікатор акаунту.</param>
        Task<AccountDto> GetAccountAsync(Guid accountId);

        /// <summary>
        /// Редагувати дані акаунту.
        /// Всі поля окрім Id опціональні — передавай тільки те що змінюєш.
        /// </summary>
        /// <param name="accountDto">DTO з оновленими даними акаунту.</param>
        Task<Result> EditAccountAsync(AccountEditDto accountDto);

        /// <summary>
        /// Видалити акаунт разом з усіма його рецептами та відгуками.
        /// </summary>
        /// <param name="accountId">Ідентифікатор акаунту для видалення.</param>
        Task<Result> DeleteAccountAsync(Guid accountId);
    }
}