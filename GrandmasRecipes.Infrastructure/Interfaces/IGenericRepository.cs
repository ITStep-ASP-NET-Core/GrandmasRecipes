namespace GrandmasRecipes.Infrastructure.Interfaces
{
    /// <summary>
    /// Базовий репозиторій для стандартних CRUD операцій.
    /// </summary>
    /// <typeparam name="T">Тип сутності.</typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>Отримати всі записи.</summary>
        Task<ICollection<T>> GetAllAsync();

        /// <summary>Отримати запис за ідентифікатором.</summary>
        /// <param name="id">Ідентифікатор запису.</param>
        Task<T?> GetByIdAsync(int id);

        /// <summary>Додати новий запис.</summary>
        /// <param name="obj">Об'єкт для додавання.</param>
        Task AddAsync(T obj);

        /// <summary>Оновити існуючий запис.</summary>
        /// <param name="obj">Об'єкт з оновленими даними.</param>
        void Update(T obj);

        /// <summary>Видалити запис.</summary>
        /// <param name="obj">Об'єкт для видалення.</param>
        void Delete(T obj);
    }
}