using Microsoft.Extensions.DependencyInjection;

namespace GrandmasRecipes.Application.ServiceProviderExtensions
{
    public static class UnitOfWorkExtensions
    {
		public static void AddUnitOfWork ( this IServiceCollection services )
		{

			//services.AddScoped<IUnitOfWork, EFUnitOfWork>();
		}
	}
}