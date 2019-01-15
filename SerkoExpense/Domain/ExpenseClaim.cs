using System;

namespace SerkoExpense.Domain
{
    public class ExpenseClaim
    {
        public ExpenseClaim(string costCentre, decimal total, string paymentMethod)
        {
            Expense = new Expense {CostCentre = costCentre, Total = total, PaymentMethod = paymentMethod};
        }

        public Expense Expense { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}