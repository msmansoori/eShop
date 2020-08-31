using System.Collections;
using System.Collections.Generic;
using eShop.ProductEntities.Entities.Enums;
using eShop.ProductEntities.Entities.HelperFields;

namespace eShop.ProductEntities.Entities
{
    public class Product : HelperField
    {
        public Product() { }

        public string Name { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public long BrandId { get; set; }
        public Brand Brand { get; set; }

        public decimal Price { get; set; }
        public double Discount { get; set; }
        public decimal DiscountPrice { get; set; }

        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Specification { get; set; }

        public string Colors { get; set; } // Multiple color seperated by ";" - Use Color enum
        public string Sizes { get; set; } // Multiple size seperated by ";" - Use Size enum

        // Quantity in stock
        public int AvailableStock { get; set; }
        public Promotion Promotion { get; set; }
        public override string Display => Name;
    }
}
