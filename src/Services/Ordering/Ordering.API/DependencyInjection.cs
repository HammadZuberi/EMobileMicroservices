

namespace Ordering.API
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddAPIServices
			(this IServiceCollection services)
		{
			// add cartr
			return services;
		}


		public static WebApplication UseAPIServices(this WebApplication app)
		{
			//mapp carter
			return app;
		}

	}
}
