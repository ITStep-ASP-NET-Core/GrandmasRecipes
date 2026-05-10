using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Cuisine> Cuisines { get; }
        IGenericRepository<Difficulty> Difficulties { get; }
        IGenericRepository<Product> Products { get; }
		IRecipeRepository Recipes { get; }
        IAccountRepository Accounts { get; }
        IReviewRepository Reviews { get; }
        IIngredientRepository Ingredients { get; }
        IRefreshTokenRepository RefreshTokens { get; }

		Task SaveChangesAsync();
    }
}