using DevHub.BLL.Core.Interface;
using DevHub.BLL.Helpers;
using DevHub.BLL.Methods;
using DevHub.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("/Inventory")]
    public class InventoryController : BaseController
    {
        private readonly IInventoryInterface _inventory;
        private readonly Validators _validate;
        private readonly HttpResponses _response;

        public InventoryController(IInventoryInterface inventory, Validators validate, HttpResponses response)
        {
            _inventory = inventory;
            _validate = validate;
            _response = response;
        }

        [HttpPost("CreateUpdate")]
        public IActionResult AddUpdate([FromBody] InventoryModel model)
        {
            model.Username = UserName;
            var validate = _validate.IsInventoryModelValid(model);
            if (validate.isValid)
            {
                var result = _inventory.CreateUpdate(model, UserName);
                return Ok(new
                {
                    data = result,
                    status = result.Action == 1? _response.ShowHttpResponse(_response.Created) : _response.ShowHttpResponse(_response.Updated)
                });
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


    }
}