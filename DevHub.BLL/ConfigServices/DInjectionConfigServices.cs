using DevHub.BLL.Core.Interface;
using DevHub.BLL.Core.Repository;
using DevHub.BLL.Helpers;
using DevHub.BLL.Methods;
using DevHub.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace DevHub.BLL.ConfigServices
{
    public static class DInjectionConfigServices
    {
        public static IServiceCollection RegisterDInjections(this IServiceCollection services)
        {
            //Methods
            services.AddTransient<DevHubContext>();
            services.AddTransient<MethodLibrary>();
            services.AddTransient<Validators>();
            services.AddTransient<HttpResponses>();
            services.AddTransient<GuidMethod>();
            services.AddTransient<QueryMethod>();
            services.AddTransient<EmailMethod>();
            services.AddTransient<CheckForgery>();

            //classes
            services.AddTransient<IBookLogInterface, BookLogRepository>();
            services.AddTransient<IClientInterface, ClientRepository>();
            services.AddTransient<ITimeTrackerInterface, TimeTrackerRepository>();
            services.AddTransient<IUserInterface, UserRepository>();
            services.AddTransient<IAccountInterface, AccountRepository>();
            services.AddTransient<IInventoryInterface, InventoryRepository>();
            services.AddTransient<IProductInterface, ProductRepository>();
            services.AddTransient<IStaticInterface, StaticRepository>();

            return services;
        }
    }
}
