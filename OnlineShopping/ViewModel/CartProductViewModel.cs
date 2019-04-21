using OnlineShopping.Models.DomainModel;

namespace OnlineShopping.ViewModel
{
    public class CartProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageSource { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }
        public int Quantity { get; set; }
        public ProductImage Image { get; set; }
        public double Amount => (SalePrice == 0) ? Price * Quantity : SalePrice * Quantity;
    }
}