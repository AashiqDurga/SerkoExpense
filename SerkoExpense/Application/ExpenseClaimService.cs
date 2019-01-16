using SerkoExpense.Infrastructure;

namespace SerkoExpense.Application
{
    public class ExpenseClaimService : IExpenseClaimService
    {
        private readonly IExpenseClaimFactory _expenseClaimFactory;
        private readonly IDataExtractor _dataExtractor;

        public ExpenseClaimService()
        {
            _dataExtractor = new EmailDataExtractor();
            _expenseClaimFactory = new ExpenseClaimFactory();
        }

        public ExpenseClaimResult Process(string email)
        {
            var expenseClaimInput = _dataExtractor.Extract(email);

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