using eShop.ProductEntities.Entities.HelperFields;

namespace eShop.ProductEntities.Entities
{
    public class Category : LibraryField
    {
        public long ParentId { get; set; }
    }
}
