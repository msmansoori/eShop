using eShop.Common.HelperFields;
using eShop.IdentityEntities.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShop.IdentityEntities.Entities
{
    public class User : BaseField
    {
        public string Name { get; set; }
        public string PersonalEmail { get; set; }
        public string BusinessEmail { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        [NotMapped]
        public string FullAddress =>
            (string.IsNullOrEmpty(Line1) ? Line1 + ", " : string.Empty) +
            (string.IsNullOrEmpty(Line2) ? Line2 + ", " : string.Empty) +
            (string.IsNullOrEmpty(City) ? City + ", " : string.Empty) +
            (string.IsNullOrEmpty(State) ? State + ", " : string.Empty) +
            (string.IsNullOrEmpty(Zipcode) ? Zipcode + ", " : string.Empty);
    }
}