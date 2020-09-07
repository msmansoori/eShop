using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using eShop.Common.Constants;
using eShop.Common.Services;
using eShop.ProductAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eShop.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<PaginatedItemsResponse> Get([Required] ProductItemsRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model");
            }
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Product.ProductClient(channel);
                _logger.LogDebug("Grpc get products request {@request}", request);
                return await client.GetProductsAsync(request);
            });
            return response;
        }

        [HttpGet, Route("{id}")]
        public async Task<ProductResponse> Get([Required] Guid id)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Product.ProductClient(channel);
                _logger.LogDebug("Grpc get product by id request {@request}", id);
                return await client.GetProductByIdAsync(new ProductItemRequest { Id = id.ToString() });
            });
            return response;
        }

        [HttpPut]
        public async Task<ProductResponse> AddProduct([Required] ProductRequest request)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Product.ProductClient(channel);
                _logger.LogDebug("Grpc add product request {@request}", request);
                var product = await client.AddProductAsync(request);
                return product;
            });
            return response;
        }

        [HttpPut]
        public async Task<ProductResponse> UpdateProduct([Required] ProductRequest request)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Product.ProductClient(channel);
                _logger.LogDebug("Grpc update product request {@request}", request);
                return await client.UpdateProductAsync(request);
            });
            return response;
        }

        [HttpDelete, Route("{id}")]
        public async Task<ProductResponse> DeleteProduct([Required] Guid id)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Product.ProductClient(channel);
                _logger.LogDebug("Grpc delete product request {@request}", id);
                return await client.DeleteProductAsync(new ProductItemRequest { Id = id.ToString() });
            });
            return response;
        }
    }
}
