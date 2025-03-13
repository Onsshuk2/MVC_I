using pv311_mvc_project.Models;

namespace pv311_mvc_project.ViewModels
{
    public class ProductCartVM
    {
        public Product Product { get; set; } = new();
        public int Quantity { get; set; } = 1;
        public int Cost { get => (int)Product.Price * Quantity; }
    }
}
