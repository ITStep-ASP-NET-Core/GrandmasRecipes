using GrandmasRecipes.Application.DTO.Category;
using GrandmasRecipes.Application.DTO.Cuisine;
using GrandmasRecipes.Application.DTO.Difficulty;
using GrandmasRecipes.Application.DTO.Product;
using GrandmasRecipes.Application.Implementations;
using GrandmasRecipes.Application.Interfaces;
using GrandmasRecipes.Infrastructure.Interfaces;
using GrandmasRecipes.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GrandmasRecipes.Application.ServiceProviderExtensions
{
    public static class UnitOfWorkExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            // Репозитории
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Сервисы
            services.AddScoped<IRecipeService, RecipeService>();
            services.AddScoped<IService<CategoryDto>, CategoryService>();
            services.AddScoped<IService<CuisineDto>, CuisineService>();
            services.AddScoped<IService<DifficultyDto>, DifficultyService>();
            services.AddScoped<IService<ProductDto>, ProductService>();
        }
    }
}