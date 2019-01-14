using System;

namespace SerkoExpense.Domain
{
    public class Expense
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}