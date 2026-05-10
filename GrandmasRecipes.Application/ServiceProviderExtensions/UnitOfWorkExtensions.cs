using GrandmasRecipes.Domain.Entities;
using GrandmasRecipes.Infrastructure.Interfaces;
using GrandmasRecipes.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GrandmasRecipes.Application.ServiceProviderExtensions
{
    public static class UnitOfWorkExtensions
    {
        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
    }
}