using Microsoft.Extensions.Logging;
using SerkoExpense.Domain;

namespace SerkoExpense.Application
{
    public class ExpenseClaimFactory : IExpenseClaimFactory
    {
        private readonly IDateValidator _validator;
        private readonly ILogger<IExpenseClaimFactory> _logger;

        public ExpenseClaimFactory(IDateValidator validator, ILogger<IExpenseClaimFactory> logger)
        {
            _validator = validator;
            _logger = logger;
        }

        public ExpenseClaim CreateExpenseClaimFrom(ExpenseClaimInput expenseInput)
        {
            var expenseClaimDate = _validator.Validate(expenseInput.Date);

            _logger.LogInformation("Date has been successfully validated");
            var expenseClaim = new ExpenseClaim(expenseInput.CostCentre, expenseInput.Total, expenseInput.PaymentMethod)
            {
                Vendor = expenseInput.Vendor, Date = expenseClaimDate, Description = expenseInput.Description
            };

            _logger.LogInformation("Expense Claim successfully created.");
            return expenseClaim;
        }
    }
}