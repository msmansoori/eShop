using System;

namespace eShop.APIGateway.Models
{
    public class MegaSale
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int Discount { get; set; }
        public DateTime Date { get; set; }
    }
}
