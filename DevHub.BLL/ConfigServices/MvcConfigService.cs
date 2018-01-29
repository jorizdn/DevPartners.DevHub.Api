using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DevHub.BLL.ConfigServices
{
    public static class MvcConfigService
    {
        public static IServiceCollection RegisterMvc(this IServiceCollection services)
        {

            services.AddMvc()
                .AddMvcOptions(options => options.MvcOptions())
                .AddJsonOptions(options => options.MvcJsonOptions());

            return services;
        }
        private static MvcJsonOptions MvcJsonOptions(this MvcJsonOptions option)
        {

            //Capitalize The First Letter of Each Word
            //            var castedResolver = option.SerializerSettings.ContractResolver as DefaultContractResolver;
            //
            //            if (castedResolver != null) {
            //                castedResolver.NamingStrategy = null;
            //            }

            option.SerializerSettings.Formatting = Formatting.Indented;
            option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //This will stop the Circular Reference.
            option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            return option;
        }

        private static MvcOptions MvcOptions(this MvcOptions option)
        {

            option.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

            //Enable SSL
            //Properties > launchSettings.json > SSL Port
            //            option.SslPort = 44327;

            //EnableSSL
            //Postman -> Settings -> Uncheck 'SSL certificate verification'.
            //            option.Filters.Add(new RequireHttpsAttribute());

            return option;
        }
    }
}