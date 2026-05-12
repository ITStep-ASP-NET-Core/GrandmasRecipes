using GrandmasRecipes.Application.DTO.Category;
using GrandmasRecipes.Application.DTO.Cuisine;
using GrandmasRecipes.Application.DTO.Difficulty;
using GrandmasRecipes.Application.DTO.Measure;
using GrandmasRecipes.Application.DTO.Product;
using GrandmasRecipes.Application.Implementations;
using GrandmasRecipes.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GrandmasRecipes.Application.ServiceProviderExtensions
{
    public static class ApplicationServicesExtensions
	{
        public static void AddApplicationServices ( this IServiceCollection services )
		{
			services.AddScoped<IRecipeService, RecipeService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<IReviewService, ReviewService>();
			services.AddScoped<ILikeService, LikeService>();
			services.AddScoped<IService<CategoryDto>, CategoryService>();
			services.AddScoped<IService<CuisineDto>, CuisineService>();
			services.AddScoped<IService<DifficultyDto>, DifficultyService>();
			services.AddScoped<IService<ProductDto>, ProductService>();
			services.AddScoped<IService<MeasureDto>, MeasureService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
		}
	}
}