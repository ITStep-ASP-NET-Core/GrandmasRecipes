using GrandmasRecipes.Infrastructure.Interfaces;
using GrandmasRecipes.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GrandmasRecipes.Infrastructure.ServiceProviderExtensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICuisineRepository, CuisineRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}