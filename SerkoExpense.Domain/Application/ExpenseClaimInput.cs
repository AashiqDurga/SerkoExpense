namespace SerkoExpense.Domain.Application
{
    public class ExpenseClaimInput
    {
        public string CostCentre { get; set; }
        public decimal Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Vendor { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }
}