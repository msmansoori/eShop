using eShop.Common.HelperFields;

namespace eShop.ProductEntities.Entities
{
    public class Basket : BaseField
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }
    }
}
