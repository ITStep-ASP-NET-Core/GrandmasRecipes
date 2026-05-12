using GrandmasRecipes.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GrandmasRecipes.Application.ServiceProviderExtensions
{
    public static class ApplicationContextExtensions
	{
        public static void AddApplicationContext ( this IServiceCollection services, string? connection )
		{
			services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
		}
	}
}