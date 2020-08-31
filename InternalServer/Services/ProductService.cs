using System;
using System.Collections.Generic;
using System.Text;
using InternalServer.Infrastructure.API;

namespace InternalServer.Services
{
    public static class ProductService
    {
        public static void GetMegaSale()
        {
            Product.GetMegaSale("");
        }
    }
}
