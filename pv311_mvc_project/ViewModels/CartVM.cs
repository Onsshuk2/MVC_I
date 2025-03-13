namespace pv311_mvc_project.ViewModels
{
    public class CartVM
    {
        public IEnumerable<ProductCartVM> Items { get; set; } = [];
        public int Shipping { get; set; } = 100;
        public int Discount { get; set; } = 0;
        public string? AppliedPromoCode { get; set; } //поле для промокоду

        public double TotalPrice => Items.Sum(i => i.Cost);
        public double TotalPriceWithDiscount => TotalPrice * (1 - (double)Discount / 100);
    }

}
