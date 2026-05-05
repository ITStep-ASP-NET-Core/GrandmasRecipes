using GrandmasRecipes.Domain.Entities;

namespace GrandmasRecipes.Infrastructure.Interfaces
{
    public interface IRecipeRepository
    {
        IQueryable<Recipe> GetAll();
        Task<Recipe?> GetByIdAsync(Guid id);
        Task AddAsync(Recipe entity);
        void Update(Recipe entity);
        void Delete(Recipe entity);

        // --- ДОБАВЬ ЭТУ СТРОКУ ---
        Task SaveChangesAsync();
        // -------------------------

        IQueryable<Recipe> GetRecipesWithAuthors();
        IQueryable<Recipe> GetRecipesByAuthor(Guid authorId);
    }
}