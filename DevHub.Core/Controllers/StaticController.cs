using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevHub.BLL.Core.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DevHub.DAL.Identity;

namespace DevHub.Core.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("/Static")]
    public class StaticController : BaseController
    {
        private readonly IStaticInterface _static;

        public StaticController(IStaticInterface statics)
        {
            _static = statics;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("Categories")]
        public IActionResult GetCategories()
        {
            return Ok(new
            {
                data = _static.GetCategories()
            });
        }

        [Authorize(Policy = "Member")]
        [HttpGet("UnitOfMeasure")]
        public IActionResult GetUnitOfMeasure()
        {
            return Ok(new
            {
                data = _static.GetUnitOfMeasure()
            });
        }


    }
}