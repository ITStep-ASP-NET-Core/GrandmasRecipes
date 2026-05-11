using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Account;
using GrandmasRecipes.Application.DTO.Category;
using GrandmasRecipes.Application.DTO.Cuisine;
using GrandmasRecipes.Application.DTO.Difficulty;
using GrandmasRecipes.Application.DTO.Ingredient;
using GrandmasRecipes.Application.DTO.Recipe;
using GrandmasRecipes.Application.DTO.Step;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly ILikeRepository _likeRepository;
		private readonly IUnitOfWork _uow;

        public RecipeService(IRecipeRepository recipeRepository, ILikeRepository likeRepository, IUnitOfWork uow)
        {
            _recipeRepository = recipeRepository;
			_likeRepository = likeRepository;
			_uow = uow;
        }

		public async Task<PagedResult<RecipePreviewDto>> GetRecipesAsync ( int page, Guid? userId = null )
		{
			var result = await _recipeRepository.GetRecipesByLikesAsync(page);
			return await MapToPreviewPaged(result, userId);
		}

		public async Task<PagedResult<RecipePreviewDto>> GetRecipesByFiltersAsync ( RecipeFilterDto filter, int page, Guid? userId = null )
		{
			var result = await _recipeRepository.GetRecipesByFiltersAsync(
				filter.CategoryIds,
				filter.CuisineIds,
				filter.DifficultyIds,
				filter.ProductIds,
				page >= 0 ? page : 0);
			return await MapToPreviewPaged(result, userId);
		}

		public async Task<PagedResult<RecipePreviewDto>> GetRecipesByAuthorAsync ( Guid authorId, int page, Guid? userId = null )
		{
			var result = await _recipeRepository.GetRecipesByAuthorAsync(authorId, page >= 0 ? page : 0);
			return await MapToPreviewPaged(result, userId);
		}

		public async Task<PagedResult<RecipePreviewDto>> GetRecipesByLikedAsync(Guid userId, int page)
        {
            var result = await _recipeRepository.GetRecipesByLikesAsync(page >= 0 ? page : 0);
            return await MapToPreviewPaged(result, userId);
        }

		public async Task<RecipeDetailsDto?> GetRecipeByIdAsync ( Guid recipeId, Guid? userId = null )
		{
			var r = await _recipeRepository.GetRecipeByIdWithAllAsync(recipeId);
			if(r == null) return null;
			return await MapToDetails(r, userId);
		}

		public async Task<Result> CreateRecipeAsync(RecipeCreateDto dto)
        {
            var recipe = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                ImageUrls = dto.ImageUrls,
                Calories = dto.Calories,
                Likes = 0,
                PublishedDate = DateTime.UtcNow,
                AuthorId = dto.AuthorId,
                DifficultyId= dto.DifficultyId,
                CuisineId = dto.CuisineId,
            };

            await _recipeRepository.AddRecipeAsync(recipe);
            await _uow.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> EditRecipeAsync(RecipeEditDto dto)
        {
            var recipe = await _recipeRepository.GetRecipeByIdWithAllAsync(dto.RecipeId);
            if (recipe == null) return Result.Fail("Рецепт не найден");

            if (dto.Title != null) recipe.Title = dto.Title;
            if (dto.Description != null) recipe.Description = dto.Description;
            if (dto.ImageUrls != null) recipe.ImageUrls = dto.ImageUrls;
            if (dto.Calories != null) recipe.Calories = dto.Calories.Value;
            if (dto.DifficultyId != null) recipe.DifficultyId= dto.DifficultyId.Value;
            if (dto.CuisineId != null) recipe.CuisineId = dto.CuisineId.Value;

            _recipeRepository.UpdateRecipe(recipe);
            await _uow.SaveChangesAsync();
            return Result.Ok();
        }

        public async Task<Result> DeleteRecipeAsync(Guid recipeId)
        {
            var recipe = await _recipeRepository.GetRecipeByIdWithAllAsync(recipeId);
            if (recipe == null) return Result.Fail("Рецепт не найден");

            _recipeRepository.DeleteRecipe(recipe);
            await _uow.SaveChangesAsync();
            return Result.Ok();
        }

		private async Task<PagedResult<RecipePreviewDto>> MapToPreviewPaged ( PagedResult<Recipe> source, Guid? userId )
		{
			HashSet<Guid> likedRecipeIds = [];

			if(userId != null)
			{
				var ids = await _likeRepository.GetLikedRecipeIdsAsync(userId.Value);
				likedRecipeIds = ids.ToHashSet();
			}

			var items = source.Items
				.Select(r => MapToPreview(r, likedRecipeIds))
				.ToList();

			return new PagedResult<RecipePreviewDto>
			{
				Items = items,
				TotalCount = source.TotalCount,
				PageNumber = source.PageNumber,
				PageSize = source.PageSize
			};
		}

		private RecipePreviewDto MapToPreview ( Recipe r, HashSet<Guid> likedRecipeIds )
		{
			return new RecipePreviewDto
			{
				Id = r.Id,
				Title = r.Title,
				ImageUrl = r.ImageUrls?.FirstOrDefault(),
				Likes = r.Likes,
				IsLiked = likedRecipeIds.Contains(r.Id),
				AuthorNickname = r.Author?.Nickname
			};
		}

		private async Task<RecipeDetailsDto> MapToDetails(Recipe r, Guid? userId)
        {
            return new RecipeDetailsDto
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                ImageUrls = r.ImageUrls,
                Calories = r.Calories,
                Likes = r.Likes,
                IsLiked = userId != null ? await _likeRepository.ExistsLikeAsync(((Guid)userId), r.Id) : false,

                Author = r.Author == null ? null : new AccountSummaryDto
                {
                    Id = r.Author.Id,
                    Nickname = r.Author.Nickname,
                    ImageUrl = null
                },

                Difficulty = r.Difficulty == null ? null : new DifficultySummaryDto
                {
                    Id = r.Difficulty.Id,
                    Name = r.Difficulty.Name
                },

                Cuisine = r.Cuisine == null ? null : new CuisineSummaryDto
                {
                    Id = r.Cuisine.Id,
                    Name = r.Cuisine.Name
                },

                Categories = r.Categories.Select(c => new CategorySummaryDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList(),

                Ingredients = r.Ingredients.Select(i => new IngredientDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name ?? string.Empty,
					MeasureId = i.MeasureId,
					Measure = i.Measure?.Name ?? string.Empty,
                    Amount = (int)i.Quantity
                }).ToList(),

                Steps = r.Steps.OrderBy(s => s.Number).Select(s => new StepDto
                {
                    Number = s.Number,
                    Title = s.Title,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    SubSteps = s.SubSteps?.OrderBy(ss => ss.Number)
                        .Select(ss => ss.Description).ToArray()
                }).ToList()
            };
        }
    }
}