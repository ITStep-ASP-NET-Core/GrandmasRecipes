using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    /// <summary>
    /// Unit of Work — збирає всі репозиторії в одному місці.
    /// Використовується в сервісах замість окремих репозиторіїв.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>Репозиторій категорій.</summary>
        IGenericRepository<Category> Categories { get; }

        /// <summary>Репозиторій кухонь.</summary>
        IGenericRepository<Cuisine> Cuisines { get; }

        /// <summary>Репозиторій рівнів складності.</summary>
        IGenericRepository<Difficulty> Difficulties { get; }

        /// <summary>Репозиторій продуктів.</summary>
        IGenericRepository<Product> Products { get; }

        /// <summary>Репозиторій одиниць виміру.</summary>
        IGenericRepository<Measure> Measures { get; }

        /// <summary>Репозиторій рецептів.</summary>
        IRecipeRepository Recipes { get; }

        /// <summary>Репозиторій акаунтів.</summary>
        IAccountRepository Accounts { get; }

        /// <summary>Репозиторій відгуків.</summary>
        IReviewRepository Reviews { get; }

        /// <summary>Репозиторій лайків.</summary>
        ILikeRepository Likes { get; }

        /// <summary>Репозиторій інгредієнтів.</summary>
        IIngredientRepository Ingredients { get; }

        /// <summary>Репозиторій refresh токенів.</summary>
        IRefreshTokenRepository RefreshTokens { get; }

        /// <summary>
        /// Зберегти всі зміни в базі даних.
        /// Викликається після всіх операцій в межах одного запиту.
        /// </summary>
        Task SaveChangesAsync();
    }
}