using EduToyRent.DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduToyRent.DataAccess.Context
{
    public static class DbContextConnection
    {
        public static IServiceCollection AddEduToyRentDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EduToyRentDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
