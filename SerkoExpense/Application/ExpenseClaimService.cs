using Microsoft.Extensions.Logging;
using SerkoExpense.Infrastructure;

namespace SerkoExpense.Application
{
    public class ExpenseClaimService : IExpenseClaimService
    {
        private readonly IExpenseClaimFactory _expenseClaimFactory;
        private readonly ILogger _logger;
        private readonly IDataExtractor _emailDataExtractor;

        public ExpenseClaimService(IDataExtractor emailEmailDataExtractor, IExpenseClaimFactory expenseClaimFactory,
            ILogger<IExpenseClaimService> logger)
        {
            _emailDataExtractor = emailEmailDataExtractor;
            _expenseClaimFactory = expenseClaimFactory;
            _logger = logger;
        }

        public ExpenseClaimResult Process(string email)
        {
            var expenseClaimInput = _emailDataExtractor.Extract(email);

            return BuildExpenseClaimResult(expenseClaimInput);
        }

        private ExpenseClaimResult BuildExpenseClaimResult(ExpenseClaimInput expenseClaimInput)
        {
            var expenseClaim = _expenseClaimFactory.CreateExpenseClaimFrom(expenseClaimInput);

            _logger.LogInformation($"Built expense claim {expenseClaim}");
            return new ExpenseClaimResult
            {
                CostCentre = expenseClaim.Expense.CostCentre, Date = expenseClaim.Date,
                Description = expenseClaim.Description, GstAmount = expenseClaim.Expense.GstAmount,
                PaymentMethod = expenseClaim.Expense.PaymentMethod,
                TotalExcludingGst = expenseClaim.Expense.TotalExcludingGst,
                TotalIncludingGst = expenseClaim.Expense.Total, Vendor = expenseClaim.Vendor
            };
        }
    }
}