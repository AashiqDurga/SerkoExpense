using System;
using Microsoft.AspNetCore.Mvc;
using SerkoExpense.Application;

namespace SerkoExpense.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseClaimService _expenseClaimService;

        public ExpenseController(IExpenseClaimService expenseClaimService)
        {
            _expenseClaimService = expenseClaimService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] string email)
        {
            try
            {
                return Ok(_expenseClaimService.Process(email));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}