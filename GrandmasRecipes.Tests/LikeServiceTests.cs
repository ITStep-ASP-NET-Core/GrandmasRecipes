using GrandmasRecipes.Application.Implementations;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace GrandmasRecipes.Tests
{
    public class LikeServiceTests
    {
        private readonly Mock<IUnitOfWork> _uow = new();
        private readonly Mock<ILikeRepository> _likeRepo = new();
        private readonly Mock<IRecipeRepository> _recipeRepo = new();
        private readonly LikeService _service;

        public LikeServiceTests()
        {
            _uow.Setup(u => u.Likes).Returns(_likeRepo.Object);
            _uow.Setup(u => u.Recipes).Returns(_recipeRepo.Object);
            _service = new LikeService(_uow.Object);
        }

        [Fact]
        public async Task AddLikeAsync_Success()
        {
            var accountId = Guid.NewGuid();
            var recipeId = Guid.NewGuid();
            var recipe = new Recipe { Id = recipeId, Likes = 0 };

            _likeRepo.Setup(r => r.ExistsLikeAsync(accountId, recipeId)).ReturnsAsync(false);
            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(recipeId)).ReturnsAsync(recipe);

            var result = await _service.AddLikeAsync(accountId, recipeId);

            Assert.True(result.Success);
            Assert.Equal(1, recipe.Likes);
            _uow.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddLikeAsync_AlreadyLiked_ReturnsFail()
        {
            var accountId = Guid.NewGuid();
            var recipeId = Guid.NewGuid();

            _likeRepo.Setup(r => r.ExistsLikeAsync(accountId, recipeId)).ReturnsAsync(true);

            var result = await _service.AddLikeAsync(accountId, recipeId);

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task AddLikeAsync_RecipeNotFound_ReturnsFail()
        {
            var accountId = Guid.NewGuid();
            var recipeId = Guid.NewGuid();

            _likeRepo.Setup(r => r.ExistsLikeAsync(accountId, recipeId)).ReturnsAsync(false);
            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(recipeId)).ReturnsAsync((Recipe?)null);

            var result = await _service.AddLikeAsync(accountId, recipeId);

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task RemoveLikeAsync_Success_DecreasesLikes()
        {
            var accountId = Guid.NewGuid();
            var recipeId = Guid.NewGuid();
            var like = new Like { AccountId = accountId, RecipeId = recipeId };
            var recipe = new Recipe { Id = recipeId, Likes = 5 };

            _likeRepo.Setup(r => r.GetLikeAsync(accountId, recipeId)).ReturnsAsync(like);
            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(recipeId)).ReturnsAsync(recipe);

            var result = await _service.RemoveLikeAsync(accountId, recipeId);

            Assert.True(result.Success);
            Assert.Equal(4, recipe.Likes);
        }

        [Fact]
        public async Task RemoveLikeAsync_LikesAtZero_DoesNotGoNegative()
        {
            var accountId = Guid.NewGuid();
            var recipeId = Guid.NewGuid();
            var like = new Like { AccountId = accountId, RecipeId = recipeId };
            var recipe = new Recipe { Id = recipeId, Likes = 0 };

            _likeRepo.Setup(r => r.GetLikeAsync(accountId, recipeId)).ReturnsAsync(like);
            _recipeRepo.Setup(r => r.GetRecipeByIdWithAllAsync(recipeId)).ReturnsAsync(recipe);

            var result = await _service.RemoveLikeAsync(accountId, recipeId);

            Assert.True(result.Success);
            Assert.Equal(0, recipe.Likes);
        }

        [Fact]
        public async Task RemoveLikeAsync_LikeNotFound_ReturnsFail()
        {
            var accountId = Guid.NewGuid();
            var recipeId = Guid.NewGuid();

            _likeRepo.Setup(r => r.GetLikeAsync(accountId, recipeId)).ReturnsAsync((Like?)null);

            var result = await _service.RemoveLikeAsync(accountId, recipeId);

            Assert.False(result.Success);
            Assert.NotNull(result.Error);
        }

        [Fact]
        public async Task IsLikedAsync_ReturnsTrue_WhenLikeExists()
        {
            var accountId = Guid.NewGuid();
            var recipeId = Guid.NewGuid();

            _likeRepo.Setup(r => r.ExistsLikeAsync(accountId, recipeId)).ReturnsAsync(true);

            var result = await _service.IsLikedAsync(accountId, recipeId);

            Assert.True(result);
        }
    }
}
