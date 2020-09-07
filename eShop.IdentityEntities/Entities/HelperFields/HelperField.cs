using eShop.Common.HelperFields;

namespace eShop.IdentityEntities.Entities.HelperFields
{
    public abstract class HelperField : BaseField
    {
        public User CreatedBy { get; set; }

        public User ModifiedBy { get; set; }

        public abstract string Display { get; }
    }
}
