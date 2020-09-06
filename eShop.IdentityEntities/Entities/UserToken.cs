using System;
using eShop.Common.HelperFields;

namespace eShop.IdentityEntities.Entities
{
    public class UserToken : BaseField
    {
        public string Token { get; set; }
        public bool IsExpired { get; set; }
        public DateTime ExpiryDate { get; set; }
        public User CreatedBy { get; set; }
    }
}