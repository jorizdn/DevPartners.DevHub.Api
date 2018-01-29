using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevHub.BLL.Core.Interface;
using DevHub.DAL.Models;
using DevHub.BLL.Helpers;
using Microsoft.AspNetCore.Antiforgery;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("/Account")]
    public class AccountController : Controller
    {
        private readonly IAccountInterface _account;
        private readonly HttpResponses _response;
        private readonly IAntiforgery _antiforgery;

        public AccountController(IAccountInterface account, HttpResponses response)
        {
            _account = account;
            _response = response;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginModel model)
        {
            var result = await _account.LoginAsync(model);
            if (result != null)
            {
                return Ok(new
                {
                    data = result
                });
            }
            else
            {
                var response = _response.ShowHttpResponse(_response.NotFound);
                response.Details = "Credentials are invalid!";
                return NotFound(new
                {
                    status = response
                });
            }
        }

        [HttpDelete("Logout")]
        public IActionResult Logout()
        {
            return Ok(new
            {
                data = _account.SignOut()
            });
        }


    }
}