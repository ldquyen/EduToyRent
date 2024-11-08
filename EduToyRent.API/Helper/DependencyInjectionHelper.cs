using EduToyRent.Service.Interfaces;
using EduToyRent.Service.Services;
using EduToyRent.Repository.Interfaces;
using EduToyRent.Repository.Repositories;
using EduToyRent.DAL.Interfaces;
using EduToyRent.DAL.Repositories;
using EduToyRent.Service.Common;

namespace EduToyRent.API.Helper
{
    public static class DependencyInjectionHelper
    {
        public static IServiceCollection AddServicesConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountRepository, AccountRepository>();
			services.AddScoped<IResetPasswordOTPRepository, ResetPasswordOTPRepository>();
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
            services.AddScoped<IPayOSService, PayOSService>();
            services.AddScoped<IReportService, ReportService>();
			services.AddScoped<GmailSender>();
            return services;
        }


    }
}
