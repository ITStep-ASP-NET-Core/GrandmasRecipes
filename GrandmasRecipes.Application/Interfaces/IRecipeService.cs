using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO;

namespace GrandmasRecipes.Application.Interfaces
{
    public interface IRecipeService
    {
        // Пагинация всех рецептов с именами авторов
        Task<PagedResult<RecipeDTO>> GetPagedRecipesAsync(int page, int size);

        // Пагинация рецептов конкретного автора
        Task<PagedResult<RecipeDTO>> GetRecipesByAuthorAsync(Guid authorId, int page, int size);

        // Получение одного рецепта по ID
        Task<RecipeDTO?> GetRecipeByIdAsync(Guid id);
    }
}