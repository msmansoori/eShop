using System;

namespace eShop.Common.HelperFields
{
    public abstract class BaseField
    {
        public long Id { get; set; }
        public bool Active { get; set; }
        public Guid ExternalId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
