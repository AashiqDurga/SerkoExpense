using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SerkoExpense.Application;

namespace SerkoExpense.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string email)
        {
            var service = new ExpenseClaimService();
            try
            {
                return Ok(service.Process(email));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}