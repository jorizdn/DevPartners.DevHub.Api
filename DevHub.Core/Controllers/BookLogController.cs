using DevHub.BLL.Core.Interface;
using DevHub.BLL.Helpers;
using DevHub.BLL.Methods;
using DevHub.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("/BookLog")]
    public class BookLogController : BaseController
    {
        private readonly IBookLogInterface _book;
        private readonly Validators _validate;
        private readonly HttpResponses _response;
        private readonly CheckForgery _checkForgery;


        public BookLogController(IBookLogInterface book, Validators validate, HttpResponses response, CheckForgery checkForgery)
        {
            _book = book;
            _validate = validate;
            _response = response;
            _checkForgery = checkForgery;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync(string Id)
        {
            var token = await _checkForgery.CheckToken();

            if (token != null)
            {
                return token;
            }

            if (string.IsNullOrEmpty(Id))
            {
                return Ok(new
                {
                    data = _book.GetBookLog(),
                    status = _response.ShowHttpResponse(_response.Ok)
                });
            }
            else
            {
                var result = _book.GetBookLogById(Id);
                if (result != null)
                {
                    return Ok(new
                    {
                        data = result,
                        status = _response.ShowHttpResponse(_response.Ok)
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        status = _response.ShowHttpResponse(_response.NotFound)
                    });
                }
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] UserInfo model)
        {
            var token = await _checkForgery.CheckToken();

            if (token != null)
            {
                return token;
            }

            var validate = _validate.IsUserBookInfoValid(model);
            if (validate.isValid)
            {
                var response = _response.ShowHttpResponse(_response.Ok);
                response.Details = validate.Message;
                var result = await _book.AddBookLogAsync(model);

                if (result.State.isValid)
                {
                    return Ok(new
                    {
                        data = result,
                        status = response
                    });
                }
                else
                {
                    response = _response.ShowHttpResponse(_response.NotFound);
                    return NotFound(new
                    {
                        data = result,
                        status = response
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

        [HttpPost("Confirm")]
        public async Task<IActionResult> ConfirmAsync(string id)
        {
            var token = await _checkForgery.CheckToken();

            if (token != null)
            {
                return token;
            }

            var result = await _book.ConfirmBookAsync(id, UserName);
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
                return NotFound(new
                {
                    Status = result
                });
            }
        }


    }
}