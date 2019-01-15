using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SerkoExpense.Application;
using SerkoExpense.Infrastructure;

namespace SerkoExpense.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        [HttpPost]
        public ActionResult<ExpenseClaimResult> Post([FromBody] string email)
        {
            var service = new ExpenseClaimService();
            try
            {
                return service.Process(email);
            }
            catch (Exception exception)
            {
                return StatusCode((int) HttpStatusCode.BadRequest);
            }
        }
    }
}