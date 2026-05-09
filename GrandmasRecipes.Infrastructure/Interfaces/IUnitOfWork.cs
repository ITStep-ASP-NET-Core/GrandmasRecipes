namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IRecipeRepository Recipes { get; }
        IAccountRepository Accounts { get; }
        ICategoryRepository Categories { get; }
        ICuisineRepository Cuisines { get; }
        IProductRepository Products { get; }
        IReviewRepository Reviews { get; }
        IIngredientRepository Ingredients { get; }

        Task SaveChangesAsync();
    }
}