using SerkoExpense.Domain;

namespace SerkoExpense.Application
{
    public interface IExpenseClaimFactory
    {
        ExpenseClaim CreateExpenseClaimFrom(ExpenseClaimInput expenseInput);
    }
}