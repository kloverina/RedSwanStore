namespace RedSwanStore.Data.ViewModels
{
    public class PurchaseViewModel
    {
        public decimal ActualPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal ResultPrice { get; set; }
        public decimal MoneyLeft { get; set; }
    }
}