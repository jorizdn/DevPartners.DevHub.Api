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
        private readonly QueryMethod _query;

        public ProductController(IProductInterface product, HttpResponses response, Validators validate, QueryMethod query)
        {
            _product = product;
            _response = response;
            _validate = validate;
            _query = query;
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

        [HttpGet("Get")]
        public IActionResult Get(int? id)
        {
            if (id > 0)
            {
                return Ok(new
                {
                    data = _product.GetById(id.Value)
                });
            }
            else
            {
                return Ok(new
                {
                    data = _product.Get()
                });
            }
        }

    }
}