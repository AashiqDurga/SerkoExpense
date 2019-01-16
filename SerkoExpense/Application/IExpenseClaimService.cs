namespace SerkoExpense.Application
{
    public interface IExpenseClaimService
    {
        ExpenseClaimResult Process(string email);
    }
}