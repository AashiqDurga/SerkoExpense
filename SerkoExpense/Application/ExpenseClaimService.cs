using SerkoExpense.Infrastructure;

namespace SerkoExpense.Application
{
    public class ExpenseClaimService : IExpenseClaimService
    {
        private readonly IExpenseClaimFactory _expenseClaimFactory;
        private readonly IDataExtractor _emailDataExtractor;

        public ExpenseClaimService(IDataExtractor emailEmailDataExtractor, IExpenseClaimFactory expenseClaimFactory)
        {
            _emailDataExtractor = emailEmailDataExtractor;
            _expenseClaimFactory = expenseClaimFactory;
        }

        public ExpenseClaimResult Process(string email)
        {
            var expenseClaimInput = _emailDataExtractor.Extract(email);

            return BuildExpenseClaimResult(expenseClaimInput);
        }

        private ExpenseClaimResult BuildExpenseClaimResult(ExpenseClaimInput expenseClaimInput)
        {
            var expenseClaim = _expenseClaimFactory.CreateExpenseClaimFrom(expenseClaimInput);

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