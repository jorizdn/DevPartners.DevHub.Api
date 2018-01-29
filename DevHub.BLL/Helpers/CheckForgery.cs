using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevHub.BLL.Helpers
{
    public class CheckForgery : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IAntiforgery _antiforgery;
        public CheckForgery(IAntiforgery antiforgery, IHttpContextAccessor accessor)
        {
            _antiforgery = antiforgery;
            _accessor = accessor;

        }
        public async Task<string> CheckForgeryToken()
        {

            try
            {
                await _antiforgery.ValidateRequestAsync(_accessor.HttpContext);

                return null;
            }
            catch (Exception ex)
            {

                return ex.ToString().Contains("X-XSRF-TOKEN") ? "TokenNotFound" : "TokenHadExpired";
            }
        }

        public async Task<BadRequestObjectResult> CheckToken()
        {

            var token = await CheckForgeryToken();

            if (token != null)
            {

                switch (token)
                {
                    case "TokenNotFound":
                        return BadRequest(new
                        {
                            error = "Token Not Found"
                        });
                    case "TokenHadExpired":
                        return BadRequest(new
                        {
                            error = "Token Had Expired"
                        });
                }

                return BadRequest(new { error = "Access Denied" });
            }

            return null;
        }
    }
}
