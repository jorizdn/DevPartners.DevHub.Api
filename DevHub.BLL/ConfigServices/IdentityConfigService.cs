using DevHub.DAL.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DevHub.BLL.ConfigServices
{
    public static class IdentityConfigService
    {
        public static IServiceCollection RegisterIdentities(this IServiceCollection services, string dbConnection)
        {

            //Identity Database Tables
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnection));

            ////Identity Configuration
            services.AddIdentity<AspNetUser, IdentityRole<string>>(opt =>
            {


            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<RoleManager<IdentityRole<string>>>();

            services.AddScoped<UserManager<AspNetUser>>();

            return services;
        }

        private static IdentityOptions IdentityOptions(this IdentityOptions option)
        {

            option.Password.RequireDigit = false;
            option.Password.RequireLowercase = false;
            option.Password.RequireUppercase = false;
            option.Password.RequireNonAlphanumeric = false;
            option.Password.RequiredLength = 6;
            return option;
        }
    }
}
