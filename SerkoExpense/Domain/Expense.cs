using System;

namespace SerkoExpense.Domain
{
    public class Expense
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public decimal GstAmount => CalculateGstAmount();
        public decimal TotalExcludingGst => CalculateTotalExcludingGst();

        private decimal CalculateTotalExcludingGst()
        {
            return Math.Round(Total - GstAmount, 2);
        }

        private decimal CalculateGstAmount()
        {
            return Math.Round(Total * 0.15m, 2);
        }
    }
}