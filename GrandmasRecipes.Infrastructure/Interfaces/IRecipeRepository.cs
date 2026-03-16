using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        IQueryable<Recipe> GetRecipesWithAuthors();
        IQueryable<Recipe> GetRecipesByAuthor(Guid authorId);
    }
}