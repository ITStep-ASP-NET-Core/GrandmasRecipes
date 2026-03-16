using GrandmasRecipes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Связь: у Аккаунта много Рецептов 
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.Author)
                .WithMany(a => a.Recipes)
                .HasForeignKey(r => r.AuthorId);

            // Связь: у Рецепта много Отзывов 
            modelBuilder.Entity<Review>()
                .HasOne(rev => rev.Recipe)
                .WithMany(r => r.Reviews)
                .HasForeignKey(rev => rev.RecipeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}