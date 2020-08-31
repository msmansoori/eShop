namespace InternalServer.Infrastructure.API
{
    public static class Order
    {
        public static string AddItemToBasket(string baseUri) => $"{baseUri}/basket/items";
    }
}
