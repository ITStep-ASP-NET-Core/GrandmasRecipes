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
			modelBuilder.Entity<Account>().UseTpcMappingStrategy();

			modelBuilder.Entity<Account>(entity =>
			{
				entity.ToTable("Accounts");
				entity.HasKey(a => a.Id);
			});

			modelBuilder.Entity<User>(entity =>
			{
				entity.ToTable("Users");
			});

			modelBuilder.Entity<Admin>(entity =>
			{
				entity.ToTable("Admins");
			});

			modelBuilder.Entity<Recipe>(entity =>
			{
				entity.HasKey(r => r.Id);

				entity.HasOne(r => r.Author)
					  .WithMany(a => a.Recipes)
					  .HasForeignKey(r => r.AuthorId)
					  .OnDelete(DeleteBehavior.SetNull);

				entity.HasOne(r => r.Difficulty)
					  .WithMany(d => d.Recipes)
					  .HasForeignKey(r => r.DifficultyId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(r => r.Cuisine)
					  .WithMany(c => c.Recipes)
					  .HasForeignKey(r => r.CuisineId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasMany(r => r.Steps)
					  .WithOne(s => s.Recipe)
					  .HasForeignKey(s => s.RecipeId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasMany(r => r.Ingredients)
					  .WithOne(i => i.Recipe)
					  .HasForeignKey(i => i.RecipeId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasMany(r => r.Reviews)
					  .WithOne(rv => rv.Recipe)
					  .HasForeignKey(rv => rv.RecipeId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<Ingredient>(entity =>
			{
				entity.HasKey(i => i.Id);

				entity.HasOne(i => i.Product)
					  .WithMany()
					  .HasForeignKey(i => i.ProductId)
					  .OnDelete(DeleteBehavior.Restrict);

				entity.HasOne(i => i.Measure)
					  .WithMany()
					  .HasForeignKey(i => i.MeasureId)
					  .OnDelete(DeleteBehavior.Restrict);
			});

			modelBuilder.Entity<Measure>(entity =>
			{
				entity.HasKey(s => s.Id);
			});

			modelBuilder.Entity<Step>(entity =>
			{
				entity.HasKey(s => s.Id);
			});

			modelBuilder.Entity<Review>(entity =>
			{
				entity.HasKey(rv => rv.Id);

				entity.HasOne(rv => rv.Account)
					  .WithMany(a => a.Reviews)
					  .HasForeignKey(rv => rv.AccountId)
					  .OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(p => p.Id);
			});

			modelBuilder.Entity<Difficulty>(entity =>
			{
				entity.HasKey(d => d.Id);
			});

			modelBuilder.Entity<Category>(entity =>
			{
				entity.HasKey(c => c.Id);

				entity.HasMany(c => c.Recipes)
					  .WithMany(r => r.Categories);
			});

			modelBuilder.Entity<Cuisine>(entity =>
			{
				entity.HasKey(c => c.Id);
			});

			modelBuilder.Entity<Like>(entity =>
			{
				entity.HasKey(l => new { l.AccountId, l.RecipeId });

				entity.HasOne(l => l.Account)
					  .WithMany(a => a.Liked)
					  .HasForeignKey(l => l.AccountId)
					  .OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(l => l.Recipe)
					  .WithMany(r => r.Liked)
					  .HasForeignKey(l => l.RecipeId)
					  .OnDelete(DeleteBehavior.Cascade);
			});
		}
    }
}