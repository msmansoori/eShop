using eShop.Common.HelperFields;

namespace eShop.IdentityEntities.Entities.HelperFields
{
    public abstract class HelperField : BaseField
    {
        public long? CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public long? ModifiedById { get; set; }
        public User ModifiedBy { get; set; }

        public abstract string Display { get; }
    }
}
