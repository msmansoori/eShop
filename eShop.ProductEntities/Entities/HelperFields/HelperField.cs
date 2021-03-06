﻿using eShop.Common.HelperFields;

namespace eShop.ProductEntities.Entities.HelperFields
{
    public abstract class HelperField : BaseField
    {
        public User CreatedBy { get; set; }      
        public User ModifiedBy { get; set; }
        public abstract string Display { get; }
    }
}
