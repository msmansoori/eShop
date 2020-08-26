using Microsoft.VisualBasic;

namespace eShop.Frontend.Models
{
    public class Product
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Rating { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public bool IsNewArrival { get; set; }
        public bool IsOutOfStock { get; set; }
        public bool HasDiscount { get; set; }
        public double Discount { get; set; }
        public double DiscountPrice => Price - Discount;

        public string ProductLabel =>
            (IsNewArrival ? "New" :
            (IsOutOfStock ? "out of stock" :
            (HasDiscount ? "Sale" : "")));

        public string ProductLabelClass =>
            (IsNewArrival ? "new" :
            (IsOutOfStock ? "stockout" :
            (HasDiscount ? "sale" : "")));
    }
}
