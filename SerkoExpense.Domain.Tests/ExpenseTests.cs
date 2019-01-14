using Xunit;

namespace SerkoExpense.Domain.Tests
{
    public class ExpenseTests
    {
        [Fact]
        public void GivenATotalWithGstIncludedWhenCreatingExpenseThenCalculateGstAmount()
        {
            var total = 1024.01m;
            var expectedGstAmount = 153.6015m;
            
            var expense = new Expense {CostCentre = "DEV002", Total= total, PaymentMethod = "card"};
            
            Assert.Equal(expectedGstAmount, expense.GstAmount);
        }
    }
}