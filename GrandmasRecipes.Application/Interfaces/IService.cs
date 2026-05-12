namespace GrandmasRecipes.Application.Interfaces
{
    /// <summary>
    /// Базовий інтерфейс сервісу для стандартних CRUD операцій.
    /// </summary>
    /// <typeparam name="T">Тип DTO з яким працює сервіс.</typeparam>
    public interface IService<T> where T : class
    {
        /// <summary>Додати новий запис.</summary>
        /// <param name="obj">DTO для додавання.</param>
        Task AddAsync(T obj);

        /// <summary>Редагувати існуючий запис.</summary>
        /// <param name="obj">DTO з оновленими даними.</param>
        Task EditAsync(T obj);

        /// <summary>Видалити запис.</summary>
        /// <param name="obj">DTO для видалення.</param>
        Task DeleteAsync(T obj);

        /// <summary>Отримати запис за ідентифікатором.</summary>
        /// <param name="id">Ідентифікатор запису.</param>
        Task<T?> GetAsync(int id);

        /// <summary>Отримати всі записи.</summary>
        Task<ICollection<T>> GetAllAsync();
    }
}