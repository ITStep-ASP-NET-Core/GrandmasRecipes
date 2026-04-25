using GrandmasRecipes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrandmasRecipes.Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    // Таблицы в базе данных
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Account> Accounts { get; set; }

    // --- ДОБАВЬ ЭТУ СТРОКУ, ЧТОБЫ УБРАТЬ ОШИБКИ ---
    public DbSet<Ingredient> Ingredients { get; set; }
    // ----------------------------------------------

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Тут можно настраивать связи, если БД станет сложнее
    }
}