using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevHub.BLL.Core.Interface;
using DevHub.BLL.Helpers;

namespace DevHub.Core.Controllers
{
    [Produces("application/json")]
    [Route("/TimeTracker")]
    public class TimeTrackerController : Controller
    {
        private readonly ITimeTrackerInterface _time;
        private readonly HttpResponses _response;

        public TimeTrackerController(ITimeTrackerInterface time, HttpResponses response)
        {
            _time = time;
            _response = response;
        }

        [HttpGet("Get")]
        public IActionResult Get(int? id)
        {
            if (id != null)
            {
                return Ok(new
                {
                    data = _time.GetTimeTrackerLogById(id.Value)
                });
            }
            else
            {
                return Ok(new
                {
                    data = _time.GetTimeTrackerLog()
                });
            }
        }

        [HttpPost("Timein/{id}")]
        public IActionResult TimeIn(string id)
        {
            var result = _time.TimeinTimeTracker(id);
            if (result.State.isValid)
            {
                return Ok(new
                {
                    data = result,
                    status = _response.ShowHttpResponse(_response.Ok)
                });
            }
            else
            {
                var response = _response.ShowHttpResponse(_response.NotFound);
                response.Details = result.State.Message;
                return NotFound(new
                {
                    status = response
                });
            }
        }

        [HttpPost("Timeout/{id}")]
        public IActionResult TimeOut(int id)
        {
            var result = _time.TimeoutTimeTracker(id);
            if (result.State.isValid)
            {
                return Ok(new
                {
                    data = result,
                    status = _response.ShowHttpResponse(_response.Ok)
                });
            }
            else
            {
                var response = _response.ShowHttpResponse(_response.NotFound);
                response.Details = result.State.Message;
                return NotFound(new
                {
                    status = response
                });
            }

        }

    }
}