using System;
using System.Threading.Tasks;
using eShop.APIGateway.Attributes;
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
    [CustomAuthorization]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<PaginatedItemsResponse> Get()
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.ProductService, logger: _logger, func: async channel =>
               {
                   var client = new Product.ProductClient(channel);
                   var request = new ProductItemsRequest { PageSize = 20, PageIndex = 1 };
                   _logger.LogDebug("Grpc get product request request {@request}", request);
                   return await client.GetProductsAsync(request);
               });
            return response;
        }

        [HttpGet, Route("{id}")]
        public Product1 GetProductById(Guid id)
        {
            return new Product1
            {
                Name = "Water resistant backpack",
                Category = "women men kid accessories cosmetic",
                Image = "/img/product/product-8.jpg",
                IsNewArrival = true,
                Rating = 5,
                Price = 80.11,
                HasDiscount = true,
                Discount = 10.11
            };
        }

        [HttpGet, Route("MegaSale")]
        public MegaSale GetMegaSale()
        {
            var sale = new MegaSale
            {
                Name = "Summer 2019",
                Discount = 45,
                Image = "/img/discount.jpg",
                Date = DateTime.Now.AddDays(5)
            };
            return sale;
        }
    }
}
