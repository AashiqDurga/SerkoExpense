using Xunit;

namespace SerkoExpense.Domain.Tests
{
    public class ExpenseTests
    {
        [Fact]
        public void GivenATotalWithGstIncludedWhenCreatingExpenseThenCalculateGstAmount()
        {
            const decimal total = 1024.01m;
            const decimal expectedGstAmount = 153.6015m;
            
            var expense = new Expense {CostCentre = "DEV002", Total= total, PaymentMethod = "card"};
            
            Assert.Equal(expectedGstAmount, expense.GstAmount);
        }
        
        [Fact]
        public void GivenATotalWithGstIncludedWhenCreatingExpenseThenCalculateTheTotalExcludingGst()
        {
            const decimal total = 1024.01m;
            const decimal expectedGstAmount = 870.41m;
            
            var expense = new Expense {CostCentre = "DEV002", Total= total, PaymentMethod = "card"};
            
            Assert.Equal(expectedGstAmount, expense.TotalExcludingGst);
        }
    }
}