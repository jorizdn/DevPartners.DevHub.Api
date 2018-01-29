using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("/Antiforgery")]
    public class AntiforgeryController : Controller
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