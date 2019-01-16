using System;
using Microsoft.AspNetCore.Mvc;
using SerkoExpense.Application;

namespace SerkoExpense.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {

        [HttpGet]
        public ActionResult Get()
        {
            return new OkObjectResult("Api is up and running.");
        }
    }
}