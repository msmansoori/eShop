using eShop.Common.HelperFields;
using System;

namespace eShop.ProductEntities.Entities
{
    public class User : BaseField
    {
        public string Name { get; set; }
        public Guid InternalReference { get; set; }
    }
}
