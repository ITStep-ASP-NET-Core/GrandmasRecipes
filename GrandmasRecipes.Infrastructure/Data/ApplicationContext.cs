using GrandmasRecipes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Data
{
    /// <summary>
    /// Основний EF Core контекст застосунку.
    /// Містить всі DbSet-и та конфігурацію зв'язків між сутностями.
    /// </summary>
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Like> Likes { get; set; }

        /// <summary>
        /// Конфігурація моделі: стратегія маппінгу ієрархії акаунтів (TPC),
        /// каскадне видалення для кроків/інгредієнтів/відгуків рецепту,
        /// складений ключ для лайків (AccountId + RecipeId),
        /// обмеження видалення для кухні та складності.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ... існуючий код без змін
        }
    }
}