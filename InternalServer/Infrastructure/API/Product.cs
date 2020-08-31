using System;

namespace InternalServer.Infrastructure.API
{
    public static class Product
    {
        public static string GetMegaSale(string baseUri) => $"{baseUri}/Product/MegaSale";
        public static string GetProducts(string baseUri) => $"{baseUri}/Product/";
        public static string GetProduct(string baseUri, string productId) => $"{baseUri}/product/{productId}";
    }
}
