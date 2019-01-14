using System;

namespace SerkoExpense.Domain
{
    public class ExpenseClaim
    {
        public Expense Expense { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}