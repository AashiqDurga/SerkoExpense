using SerkoExpense.Infrastructure;

namespace SerkoExpense.Application
{
    public class ExpenseClaimService
    {
        public ExpenseClaimResult Process(string email)
        {
            var dataExtractor = new EmailDataExtractor();
            var expenseClaimInput = dataExtractor.ExtractFrom(email);

            return BuildExpenseClaimResult(expenseClaimInput);
        }

        private static ExpenseClaimResult BuildExpenseClaimResult(ExpenseClaimInput expenseClaimInput)
        {
            var factory = new ExpenseClaimFactory();
            var expenseClaim = factory.CreateExpenseClaimFrom(expenseClaimInput);

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