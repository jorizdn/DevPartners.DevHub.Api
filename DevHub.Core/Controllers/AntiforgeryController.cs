using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("/Antiforgery")]
    public class AntiforgeryController : BaseController
    {
        private readonly IAntiforgery _antiforgery;
        public AntiforgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;

        }
        [HttpGet]
        public IActionResult Token()
        {
            var tokens = _antiforgery.GetAndStoreTokens(HttpContext);

            return Ok(new
            {
                antiforgerytoken = new
                {
                    token = tokens.RequestToken,
                    tokenName = tokens.HeaderName
                }
            });
        }
    }
}