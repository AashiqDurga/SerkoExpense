using SerkoExpense.Domain;
using Xunit;

namespace SerkoExpense.Tests
{
    public class ExpenseTests
    {
        private readonly Expense _expense;

        public ExpenseTests()
        {
            const decimal total = 1024.01m;
            _expense = new Expense {CostCentre = "DEV002", Total= total, PaymentMethod = "card"};
        }
        
        [Fact]
        public void GivenATotalWithGstIncludedWhenCreatingExpenseThenCalculateGstAmount()
        {
            const decimal expectedGstAmount = 153.60m;
            
            Assert.Equal(expectedGstAmount, _expense.GstAmount);
        }
        
        [Fact]
        public void GivenATotalWithGstIncludedWhenCreatingExpenseThenCalculateTheTotalExcludingGst()
        {
            const decimal expectedTotalExcludingGst = 870.41m;
            
            Assert.Equal(expectedTotalExcludingGst, _expense.TotalExcludingGst);
        }
    }
}