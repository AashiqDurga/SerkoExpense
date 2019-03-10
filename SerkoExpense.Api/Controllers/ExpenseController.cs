using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerkoExpense.Application;

namespace SerkoExpense.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseClaimService _expenseClaimService;
        private readonly ILogger _logger;

        public ExpenseController(IExpenseClaimService expenseClaimService, ILogger<ExpenseController> logger)
        {
            _expenseClaimService = expenseClaimService;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult Post([FromBody] string email)
        {
            try
            {
                _logger.LogInformation($"Processing email: {email}");
                return Ok(_expenseClaimService.Process(email));
            }
            catch (Exception exception)
            {
                _logger.LogError($"Cannot process Expense email: {exception}");
                return BadRequest(exception);
            }
        }
    }
}