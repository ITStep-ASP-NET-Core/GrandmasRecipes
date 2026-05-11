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
		public IGenericRepository<Measure> Measures { get; }
		public IRecipeRepository Recipes { get; }
        public IAccountRepository Accounts { get; }
        public IReviewRepository Reviews { get; }
        public ILikeRepository Likes { get; }
		public IIngredientRepository Ingredients { get; }
		public IRefreshTokenRepository RefreshTokens { get; }

		public UnitOfWork
        (
            ApplicationContext context,
			IGenericRepository<Category> categories,
			IGenericRepository<Cuisine> cuisines,
			IGenericRepository<Difficulty> difficulties,
			IGenericRepository<Product> products,
			IGenericRepository<Measure> measures,
			IRecipeRepository recipes,
            IAccountRepository accounts,
            IReviewRepository reviews,
            ILikeRepository likes,
			IIngredientRepository ingredients,
			IRefreshTokenRepository refreshTokens

		)
        {
            _context = context;
			Categories = categories;
			Cuisines = cuisines;
			Difficulties = difficulties;
			Products = products;
			Measures = measures;
			Recipes = recipes;
            Accounts = accounts;
            Reviews = reviews;
            Likes = likes;
			Ingredients = ingredients;
			RefreshTokens = refreshTokens;
		}

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}