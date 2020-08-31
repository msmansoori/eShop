using eShop.Common.HelperFields;

namespace eShop.ProductEntities.Entities
{
    public class User : BaseField
    {
        public string Name { get; set; }
        public long InternalReference { get; set; }
    }
}
