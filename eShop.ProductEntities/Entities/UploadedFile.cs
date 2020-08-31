using eShop.ProductEntities.Entities.Enums;
using eShop.ProductEntities.Entities.HelperFields;

namespace eShop.ProductEntities.Entities
{
    public class UploadedFile : HelperField
    {
        public UploadedFile() { }

        public string Name { get; set; }
        public string FilePath { get; set; }

        public long EntityId { get; set; }
        public Entity Entity { get; set; }

        public override string Display => Name;
    }
}
