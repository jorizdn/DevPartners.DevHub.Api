using DevHub.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DevHub.BLL.ConfigServices
{
    public static class SqlConfigService
    {
        public static IServiceCollection RegisterSqlServer(this IServiceCollection services, string dbConnection, string identityConnection)
        {

            services.AddDbContext<DevHubContext>(options => options.UseSqlServer(dbConnection));
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(identityConnection));
            return services;
        }
    }
}
