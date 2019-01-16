using SerkoExpense.Application;

namespace SerkoExpense.Infrastructure
{
    public interface IDataExtractor
    {
        ExpenseClaimInput Extract(string email);
    }
}