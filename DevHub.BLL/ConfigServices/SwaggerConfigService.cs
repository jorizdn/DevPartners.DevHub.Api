using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DevHub.BLL.ConfigServices
{
    public static class SwaggerConfigService
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection services)
        {

            return services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "DevHub API Test Environment",
                    Version = "January 17"
                });
            });
        }

        public static IApplicationBuilder RegisterSwagger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();

            applicationBuilder.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "docs/swagger";
            });

            return applicationBuilder;
        }
    }
}