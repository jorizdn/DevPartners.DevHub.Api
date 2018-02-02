using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevHub.BLL.ConfigServices
{
    public static class JWTsConfigService
    {
        public static IServiceCollection RegisterJwtAuth(this IServiceCollection services)
        {
            services.AddAuthentication()
                 .AddJwtBearer(jwt => 
                 {
                     jwt.TokenValidationParameters = new TokenValidationParameters()
                     {
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())),
                         ValidIssuer = "http://localhost:61732",
                         ValidAudience = "http://localhost:61732",
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true
                     };
                 });

            services.AddAuthorization(cfg => 
            {
                cfg.AddPolicy("Admin", a => a.RequireClaim("IsAdmin", "True"));
                cfg.AddPolicy("Member", a => a.RequireClaim("IsMember", "True"));
                cfg.InvokeHandlersAfterFailure = true;    
            });

            return services;
        }
    }
}
