using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DevHub.BLL.ConfigServices
{
    public static class CookiesConfigService
    {
        public static IServiceCollection RegisterCookieAuthentication(this IServiceCollection services)
        {

            services.Configure<IdentityOptions>(options => options.IdentityOptions());
            services.ConfigureApplicationCookie(options => {
                options.ExpireTimeSpan = TimeSpan.FromDays(3);
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {

                        //This will stop from redirecting to Login Page like the MVC.

                        if (ctx.Request.Path.StartsWithSegments("/devhubapi") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                        }

                        return Task.CompletedTask;
                    },
                    OnRedirectToAccessDenied = ctx =>
                    {

                        if (ctx.Request.Path.StartsWithSegments("/devhubapi") && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 403;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
        private static IdentityOptions IdentityOptions(this IdentityOptions config)
        {

            config.Password.RequireDigit = false;
            config.Password.RequireLowercase = false;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequireUppercase = false;
            config.Password.RequiredLength = 5;

            return config;
        }
    }
}