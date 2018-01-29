using Microsoft.AspNetCore.Builder;

namespace DevHub.BLL.ConfigApp
{
    public static class BaseAppConfig {
        public static IApplicationBuilder SetAppConfig(this IApplicationBuilder builder) {

            //This must come first before 'UseMvc'. Order is very important.
            builder.UseIdentity();
//
//            builder.UseGoogleAuthentication(new GoogleOptions() {
//                ClientId = "471143278329-87bjf09rtobdri7jdj0f6m578eebti92.apps.googleusercontent.com",
//                ClientSecret = "3TzkQ-9ZOWdR_ZWdpiLO7r8s",
//                CallbackPath = new PathString("/login/ExternalLoginCallback")
//            });
//            
            builder.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=About}/{id?}");
            });

            return builder;
        }
    }
}