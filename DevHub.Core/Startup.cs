using DevHub.BLL.ConfigApp;
using DevHub.BLL.ConfigServices;
using DevHub.BLL.Helpers;
using DevHub.BLL.Middlewares;
using DevHub.DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DevHub.Core
{
    public class Startup
    {

        private static string _dbConnection;
        private static string _identityConnection;
        private static IConfigurationRoot _configurationRoot;

        public Startup(IHostingEnvironment env)
        {
            var dalfolder = PathHelper.GetParentFolder(env);

            var builder =
                new ConfigurationBuilder().SetBasePath(dalfolder)
                    .AddJsonFile($"PublishSettings\\appSettings.{env.EnvironmentName}.json", false, true);
            _configurationRoot = builder.Build();
            _dbConnection = _configurationRoot.GetConnectionString("DefaultConnection");
            _identityConnection = _configurationRoot.GetConnectionString("IdentityConnection");
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.RegisterMvc();
            services.RegisterMapper();
            services.RegisterIdentities(_identityConnection);
            services.RegisterSqlServer(_dbConnection, _identityConnection);
            services.RegisterDInjections();
            services.RegisterCookieAuthentication();
            services.RegisterAntiForgery();
            services.RegisterSwagger();

            services.Configure<AppSettingModel>(_configurationRoot.GetSection("SwaggerAuthentication"));
            services.Configure<AppSettingModel>(_configurationRoot.GetSection("Email"));
            services.Configure<AppSettingModel>(_configurationRoot.GetSection("WebConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
               builder.WithOrigins("http://localhost").AllowAnyHeader()
               );

            //This will explicitly throw an text error with corresponding error code.
            app.UseStatusCodePages();

            app.UseSwaggerAuthentication();

            //Contains UseMvc
            app.SetAppConfig();

            app.RegisterSwagger();
        }
    }
}
