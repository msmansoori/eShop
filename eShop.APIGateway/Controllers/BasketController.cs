using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using eShop.APIGateway.Models;
using eShop.Common.Constants;
using eShop.Common.Services;
using eShop.ProductAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eShop.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly ILogger<BasketController> _logger;

        public BasketController(ILogger<BasketController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<BasketResponse> AddToBasket([Required] BasketRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model");
            }
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.ProductService, logger: _logger, func: async channel =>
            {
                var client = new Basket.BasketClient(channel);
                _logger.LogDebug("Grpc add backet {@request}", request);
                return await client.AddToBacketAsync(request);
            });
            return response;
        }

        [HttpPut]
        public async Task<BasketResponse> RemoveToBasket([Required] BasketRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model");
            }
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.ProductService, logger: _logger, func: async channel =>
            {
                var client = new Basket.BasketClient(channel);
                _logger.LogDebug("Grpc remove backet {@request}", request);
                return await client.RemoveFromBasketAsync(request);
            });
            return response;
        }
    }
}
