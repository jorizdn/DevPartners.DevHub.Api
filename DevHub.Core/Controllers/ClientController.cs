using DevHub.BLL.Core.Interface;
using DevHub.BLL.Helpers;
using DevHub.BLL.Methods;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("/Client")]
    public class ClientController : BaseController
    {
        private readonly IClientInterface _client;
        private readonly Validators _validate;
        private readonly HttpResponses _response;
        private readonly IAntiforgery _antiforgery;
        private readonly CheckForgery _checkForgery;

        public ClientController(IClientInterface client, Validators validate, HttpResponses response, IAntiforgery antiforgery, CheckForgery checkForgery)
        {
            _client = client;
            _validate = validate;
            _response = response;
            _antiforgery = antiforgery;
            _checkForgery = checkForgery;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync(string email, int? id)
        {
            //if (!IsInRole)
            //{
            //    return BadRequest(new
            //    {
            //        status = _response.ShowHttpResponse(_response.Unauthorized)
            //    });
            //}

            if (!string.IsNullOrEmpty(email))
            {
                var validate = _validate.IsEmailValid(email);
                if (validate.isValid)
                {
                    var result = _client.GetClientByEmail(email);
                    if (result != null)
                    {
                        return Ok(new
                        {
                            data = result,
                            status = _response.ShowHttpResponse(_response.Ok),
                            IsExists = true
                        });
                    }
                    else
                    {
                        return NotFound(new
                        {
                            status = _response.ShowHttpResponse(_response.NotFound),
                            IsExists = false
                        });
                    }
                }
                else
                {
                    var response = _response.ShowHttpResponse(_response.UnprocessableEntity);
                    response.Details = validate.Message;
                    return BadRequest(new
                    {
                        status = response
                    });
                }
            }
            else
            {
                try
                {
                    if (id.Value > 0)
                    {
                        var result = _client.GetClientById(id.Value);
                        if (result != null)
                        {
                            var responseOk = _response.ShowHttpResponse(_response.Ok);
                            return Ok(new
                            {
                                data = result,
                                status = responseOk,
                                IsExists = true
                            });
                        }
                        else
                        {
                            return NotFound(new
                            {
                                status = _response.ShowHttpResponse(_response.NotFound),
                                IsExists = false
                            });
                        }
                    }
                }
                catch (System.Exception)
                {
                    return Ok(new
                    {
                        data = _client.GetClients(),
                        status = _response.ShowHttpResponse(_response.Ok)
                    });
                }

                return null;
            }
        }

    }
}