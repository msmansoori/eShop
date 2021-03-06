﻿using eShop.Common.Enums;
using eShop.Common.HelperFields;
using System;

namespace eShop.ProductEntities.Entities
{
    public class User : BaseField
    {
        public string Name { get; set; }
        public UserType UserType { get; set; }
        public Guid InternalReference { get; set; }
    }
}
