using System;

namespace SerkoExpense.Application
{
    public class ExpenseClaimResult
    {
        public string CostCentre { get; set; }
        public decimal GstAmount { get; set; }
        public decimal TotalIncludingGst { get; set; }
        public decimal TotalExcludingGst { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}