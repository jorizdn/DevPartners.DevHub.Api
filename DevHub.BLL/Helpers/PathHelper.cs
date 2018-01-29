using Microsoft.AspNetCore.Hosting;

namespace DevHub.BLL.Helpers
{
    public static class PathHelper {
        public static string GetParentFolder(IHostingEnvironment env) {

            var parentfolder = env.ContentRootPath.Replace(env.ApplicationName, "DevHub.DAL");

            return parentfolder;
        }
    }
}