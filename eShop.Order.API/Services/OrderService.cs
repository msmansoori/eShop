using System.Threading.Tasks;
using eShop.OrderAPI;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace eShop.Order.API
{
    public class OrderService : OrderAPI.Order.OrderBase
    {
        private readonly ILogger<BasketService> _logger;
        public OrderService(ILogger<BasketService> logger)
        {
            _logger = logger;
        }

        public override Task<OrderReply> AddOrder(OrderRequest request, ServerCallContext context)
        {
            return Task.FromResult(new OrderReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
