using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Principal;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/Base")]
    public class BaseController : Controller
    {
        private const string Urlhelper = "UrlHelper";
        protected string UserName;
        protected bool IsInRole = false;
        protected IIdentity UserIdentity;
        protected string UserRoleString = "";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserName = HttpContext.User.Identity.Name;
            UserIdentity = HttpContext.User.Identity;
            IsInRole = HttpContext.User.IsInRole("Admin");
            context.HttpContext.Items[Urlhelper] = Url;
        }
    }
}