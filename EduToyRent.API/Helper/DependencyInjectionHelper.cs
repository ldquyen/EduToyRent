using EduToyRent.Service.Interfaces;
using EduToyRent.Service.Services;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Repository.Repositories;

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
			      services.AddScoped<ICartService, CartService>();
            services.AddScoped<IOrderService, OrderService>();  
            services.AddScoped<IVoucherService, VoucherService>();
          
            return services;
        }
    }
}
