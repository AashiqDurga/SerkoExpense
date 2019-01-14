namespace SerkoExpense.Domain
{
    public class Expense
    {
        public string CostCentre { get; set; }
        public decimal Total { private get; set; }
        public string PaymentMethod { get; set; }
        public decimal GstAmount => CalculateGstAmount();

        private decimal CalculateGstAmount()
        {
            return Total * 0.15m;
        }
    }
}