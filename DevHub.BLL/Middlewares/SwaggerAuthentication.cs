using DevHub.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace DevHub.BLL.Middlewares
{
    public class SwaggerAuthentication
    {
        private readonly IHostingEnvironment _environment;
        private readonly RequestDelegate _next;
        private readonly IOptions<AppSettingModel> _options;

        public SwaggerAuthentication(RequestDelegate next, IHostingEnvironment environment,
            IOptions<AppSettingModel> options)
        {
            _next = next;
            _environment = environment;
            _options = options;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            var url = httpContext.Request.Headers.FirstOrDefault(c => c.Key == "Referer").Value;

            if (url.ToString().Contains("swagger"))
            {

                var querystring =
                    QueryHelpers.ParseQuery(url).FirstOrDefault(c => c.Key.Contains("api_key")).Value.ToString();

                if (querystring == "")
                {

                    httpContext.Response.StatusCode = 401; //Unauthorized
                    return;
                }
                if (string.CompareOrdinal(querystring, _options.Value.ApiKey) == 0)
                {

                }
                else
                {
                    httpContext.Response.StatusCode = 401; //Unauthorized
                    return;
                }
            }

            await _next.Invoke(httpContext);
        }
    }

    public static class SwaggerAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerAuthentication>();
        }
    }
}