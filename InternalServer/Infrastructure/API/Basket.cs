namespace InternalServer.Infrastructure.API
{
    public static class Basket
    {
        public static string AddItemToBasket(string baseUri) => $"{baseUri}/basket/items";
    }
}
