using EduToyRent.BLL.Interfaces;
using EduToyRent.BLL.Services;
using EduToyRent.DAL.Interfaces;
using EduToyRent.DAL.Repositories;

namespace EduToyRent.API.Helper
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            //service
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IToyService, ToyService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IRequestFromService, RequestFormService>();
            return services;
        }
    }
}
