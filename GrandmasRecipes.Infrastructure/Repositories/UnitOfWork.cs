using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public IRecipeRepository Recipes { get; }
        public IAccountRepository Accounts { get; }
        public ICategoryRepository Categories { get; }
        public ICuisineRepository Cuisines { get; }
        public IProductRepository Products { get; }
        public IReviewRepository Reviews { get; }
        public IIngredientRepository Ingredients { get; }

        public UnitOfWork(
            ApplicationContext context,
            IRecipeRepository recipes,
            IAccountRepository accounts,
            ICategoryRepository categories,
            ICuisineRepository cuisines,
            IProductRepository products,
            IReviewRepository reviews,
            IIngredientRepository ingredients)
        {
            _context = context;
            Recipes = recipes;
            Accounts = accounts;
            Categories = categories;
            Cuisines = cuisines;
            Products = products;
            Reviews = reviews;
            Ingredients = ingredients;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}