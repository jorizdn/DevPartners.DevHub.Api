using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevHub.BLL.Core.Interface;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("/User")]
    public class UserController : Controller
    {
        private readonly IUserInterface _user;

        public UserController(IUserInterface user)
        {
            _user = user;
        }

        [HttpGet("Add")]
        public IActionResult Add()
        {
            return Ok(new
            {
                data = _user.AddAsync()
            });
        }

    }
}