using EduToyRent.Service.Interfaces;
using EduToyRent.Service.Services;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Repository.Repositories;
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
			services.AddScoped<ICartRepository, CartRepository>();
			services.AddScoped<ICartItemRepository, CartItemRepository>();

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
