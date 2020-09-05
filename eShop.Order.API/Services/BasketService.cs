using System.Threading.Tasks;
using eShop.OrderAPI;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace eShop.Order.API
{
    public class BasketService : Basket.BasketBase
    {
        private readonly ILogger<BasketService> _logger;
        public BasketService(ILogger<BasketService> logger)
        {
            _logger = logger;
        }
        public override Task<BasketReply> AddBasket(BasketRequest request, ServerCallContext context)
        {
            return Task.FromResult(new BasketReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
