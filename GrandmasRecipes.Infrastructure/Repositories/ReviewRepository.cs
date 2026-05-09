using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Data;
using GrandmasRecipes.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationContext _context;

        public ReviewRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IQueryable<Review> GetAll()
            => _context.Reviews.AsNoTracking();

        public async Task<Review?> GetByIdAsync(int id)
            => await _context.Reviews.FindAsync(id);

        public IQueryable<Review> GetByRecipeId(Guid recipeId)
            => _context.Reviews
                .Where(r => r.RecipeId == recipeId)
                .Include(r => r.Account)
                .AsNoTracking();

        public IQueryable<Review> GetByAccountId(Guid accountId)
            => _context.Reviews
                .Where(r => r.AccountId == accountId)
                .Include(r => r.Recipe)
                .AsNoTracking();

        public async Task AddAsync(Review entity)
            => await _context.Reviews.AddAsync(entity);

        public void Update(Review entity)
            => _context.Reviews.Update(entity);

        public void Delete(Review entity)
            => _context.Reviews.Remove(entity);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}