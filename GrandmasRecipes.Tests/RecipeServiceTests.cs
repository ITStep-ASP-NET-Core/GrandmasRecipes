using GrandmasRecipes.Application.DTO.Recipe;
using GrandmasRecipes.Application.Implementations;
using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace GrandmasRecipes.Tests
{
    public class RecipeServiceTests
    {
        private readonly Mock<IRecipeRepository> _recipeRepo = new();
        private readonly Mock<ILikeRepository> _likeRepo = new();
        private readonly Mock<IUnitOfWork> _uow = new();
        private readonly RecipeService _service;

        public RecipeServiceTests()
        {
            _service = new RecipeService(_recipeRepo.Object, _likeRepo.Object, _uow.Object);
        }

        [Fact]
        public async Task GetRecipeByIdAsync_RecipeExists_ReturnsDetails()
        {
            var id = Guid.NewGuid();
            var recipe = new Recipe
            {
                Id = id,
                Title = "Борщ",
                Ingredients = new List<Ingredient>(),
                Steps = new List<Step>(),
                Categories = new List<Category>()
            };

            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(id)).ReturnsAsync(recipe);

            var result = await _service.GetRecipeByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal("Борщ", result.Title);
        }

        [Fact]
        public async Task GetRecipeByIdAsync_NotFound_ReturnsNull()
        {
            var id = Guid.NewGuid();
            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(id)).ReturnsAsync((Recipe?)null);

            var result = await _service.GetRecipeByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateRecipeAsync_ValidDto_ReturnsSuccess()
        {
            var dto = new RecipeCreateDto
            {
                Title = "Борщ",
                AuthorId = Guid.NewGuid(),
                DifficultyId = 1,
                CuisineId = 1,
                Calories = 300
            };

            _recipeRepo.Setup(r => r.AddRecipeAsync(It.IsAny<Recipe>())).Returns(Task.CompletedTask);

            var result = await _service.CreateRecipeAsync(dto);

            Assert.True(result.Item2.Success);
            _uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditRecipeAsync_RecipeExists_UpdatesFields()
        {
            var id = Guid.NewGuid();
            var recipe = new Recipe
            {
                Id = id,
                Title = "Старий",
                Ingredients = new List<Ingredient>(),
                Steps = new List<Step>(),
                Categories = new List<Category>()
            };
            var dto = new RecipeEditDto { RecipeId = id, Title = "Новий" };

            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(id)).ReturnsAsync(recipe);

            var result = await _service.EditRecipeAsync(dto);

            Assert.True(result.Success);
            Assert.Equal("Новий", recipe.Title);
            _uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task EditRecipeAsync_NotFound_ReturnsFail()
        {
            var dto = new RecipeEditDto { RecipeId = Guid.NewGuid(), Title = "Новий" };
            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(dto.RecipeId)).ReturnsAsync((Recipe?)null);

            var result = await _service.EditRecipeAsync(dto);

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task DeleteRecipeAsync_RecipeExists_ReturnsSuccess()
        {
            var id = Guid.NewGuid();
            var recipe = new Recipe
            {
                Id = id,
                Ingredients = new List<Ingredient>(),
                Steps = new List<Step>(),
                Categories = new List<Category>()
            };

            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(id)).ReturnsAsync(recipe);

            var result = await _service.DeleteRecipeAsync(id);

            Assert.True(result.Success);
            _recipeRepo.Verify(r => r.DeleteRecipe(recipe), Times.Once);
            _uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteRecipeAsync_NotFound_ReturnsFail()
        {
            var id = Guid.NewGuid();
            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(id)).ReturnsAsync((Recipe?)null);

            var result = await _service.DeleteRecipeAsync(id);

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task GetRecipesAsync_ReturnsPagedResult()
        {
            var paged = new PagedResult<Recipe>
            {
                Items = new List<Recipe>(),
                TotalCount = 0,
                PageNumber = 1,
                PageSize = 10
            };

            _recipeRepo.Setup(r => r.GetRecipesByLikesAsync(1)).ReturnsAsync(paged);
            _likeRepo.Setup(r => r.GetLikedRecipeIdsAsync(It.IsAny<Guid>())).ReturnsAsync(new List<Guid>());

            var result = await _service.GetRecipesAsync(1);

            Assert.NotNull(result);
            Assert.Empty(result.Items);
        }
    }
}
