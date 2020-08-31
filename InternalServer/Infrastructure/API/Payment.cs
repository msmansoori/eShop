namespace InternalServer.Infrastructure.API
{
    public static class Payment
    {
        public static string AddItemToBasket(string baseUri) => $"{baseUri}/basket/items";
    }
}
