using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

		public IGenericRepository<Category> Categories { get; }
		public IGenericRepository<Cuisine> Cuisines { get; }
		public IGenericRepository<Difficulty> Difficulties { get; }
		public IGenericRepository<Product> Products { get; }
		public IRecipeRepository Recipes { get; }
        public IAccountRepository Accounts { get; }
        public IReviewRepository Reviews { get; }
        public IIngredientRepository Ingredients { get; }
		public IRefreshTokenRepository RefreshTokens { get; }

		public UnitOfWork
        (
            ApplicationContext context,
			GenericRepository<Category> categories,
			GenericRepository<Cuisine> cuisines,
			GenericRepository<Difficulty> difficulties,
			GenericRepository<Product> products,
			IRecipeRepository recipes,
            IAccountRepository accounts,
            IReviewRepository reviews,
            IIngredientRepository ingredients,
			IRefreshTokenRepository refreshTokens

		)
        {
            _context = context;
			Categories = categories;
			Cuisines = cuisines;
			Difficulties = difficulties;
			Products = products;
			Recipes = recipes;
            Accounts = accounts;
            Reviews = reviews;
            Ingredients = ingredients;
			RefreshTokens = refreshTokens;
		}

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}