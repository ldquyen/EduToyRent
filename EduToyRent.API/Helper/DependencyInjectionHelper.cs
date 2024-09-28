namespace EduToyRent.API.Helper
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}
