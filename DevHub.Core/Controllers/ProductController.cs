using DevHub.BLL.Core.Interface;
using DevHub.BLL.Helpers;
using DevHub.BLL.Methods;
using DevHub.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("/Product")]
    public class ProductController : BaseController
    {
        private readonly IProductInterface _product;
        private readonly HttpResponses _response;
        private readonly Validators _validate;

        public ProductController(IProductInterface product, HttpResponses response, Validators validate)
        {
            _product = product;
            _response = response;
            _validate = validate;
        }

        [HttpPost("CreateUpdate")]
        public IActionResult CreateUpdate([FromBody] ProductModel model)
        {
            var result = _product.CreateUpdate(model);
            return Ok(new
            {
                data = result,
                states = result.Action == 1 ? _response.ShowHttpResponse(_response.Created) : _response.ShowHttpResponse(_response.Updated)
            });
        }
    }
}