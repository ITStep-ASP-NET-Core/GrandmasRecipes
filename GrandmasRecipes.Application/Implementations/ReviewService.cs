using GrandmasRecipes.Application.Common;
using GrandmasRecipes.Application.DTO.Review;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;

namespace GrandmasRecipes.Application.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _uow;

        public ReviewService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<PagedResult<ReviewDto>> GetReviewsAsync(Guid recipeId, int page)
        {
            var result = await _uow.Reviews.GetReviewsByRecipeIdAsync(recipeId, page);

            return new PagedResult<ReviewDto>
            {
                Items = result.Items.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    Comment = r.Comment,
                    AuthorId = r.AccountId,
                    AuthorNickname = r.Account?.Nickname ?? string.Empty,
                    AuthorImageUrl = null
                }).ToList(),
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                PageSize = result.PageSize
            };
        }

        public async Task<Result> CreateReviewAsync(ReviewCreateDto dto)
        {
            var review = new Review
            {
                AccountId = dto.AuthorId,
                Comment = dto.Comment,
                SendingDate = DateTime.UtcNow
            };

            await _uow.Reviews.AddReviewAsync(review);
            await _uow.SaveChangesAsync();
            return Result.Ok();
        }
    }
}