using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Application.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<PagedResult<RecipeDTO>> GetPagedRecipesAsync(int page, int size)
        {
            var query = _recipeRepository.GetRecipesWithAuthors(); // Теперь эта ошибка уйдет
            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * size).Take(size)
                .Select(r => new RecipeDTO { Id = r.Id, Title = r.Title, LikesCount = r.LikesCount })
                .ToListAsync();

            return new PagedResult<RecipeDTO> { Items = items, TotalCount = total, PageNumber = page, PageSize = size };
        }

        public async Task<PagedResult<RecipeDTO>> GetRecipesByAuthorAsync(Guid authorId, int page, int size)
        {
            var query = _recipeRepository.GetRecipesByAuthor(authorId); // Теперь эта ошибка уйдет
            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * size).Take(size)
                .Select(r => new RecipeDTO { Id = r.Id, Title = r.Title })
                .ToListAsync();

            return new PagedResult<RecipeDTO> { Items = items, TotalCount = total, PageNumber = page, PageSize = size };
        }

        public async Task<RecipeDTO?> GetRecipeByIdAsync(Guid id)
        {
            var r = await _recipeRepository.GetByIdAsync(id); // Теперь эта ошибка уйдет
            return r == null ? null : new RecipeDTO { Id = r.Id, Title = r.Title };
        }
    }
}