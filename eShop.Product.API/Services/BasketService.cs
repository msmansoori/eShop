using System;
using System.Linq;
using System.Threading.Tasks;
using eShop.Common.Utilities;
using eShop.InternalServer.Interfaces;
using eShop.ProductAPI;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace eShop.Product.API.Services
{
    public class BasketService : ProductAPI.Basket.BasketBase
    {
        private readonly ILogger<BasketService> _logger;
        private readonly IMicroKernel _kernel;
        private readonly IAudience _audience;
        public BasketService(ILogger<BasketService> logger, IMicroKernel kernel, IAudience audience)
        {
            _logger = logger;
            _kernel = kernel;
            _audience = audience;
        }

        public override Task<BasketResponse> AddToBacket(BasketRequest request, ServerCallContext context)
        {
            if (request.ProductId.IsEmpty())
            {
                throw new Exception("Invalid product");
            }

            var product = _kernel.GetEntity<eShop.ProductEntities.Entities.Product>(externalId: Guid.Parse(request.ProductId));

            if (product.IsNull())
            {
                throw new Exception("Invalid product");
            }
            else
            {
                _kernel.AddEntity<eShop.ProductEntities.Entities.Basket>(new eShop.ProductEntities.Entities.Basket
                {
                    Product = product
                }, saveChanges: true);
            }

            return Task.FromResult(new BasketResponse
            {
                ProductId = product.ExternalId.ToString()
            });
        }

        public override Task<BasketResponse> RemoveFromBasket(BasketRequest request, ServerCallContext context)
        {
            if (request.ProductId.IsEmpty())
            {
                throw new Exception("Invalid product");
            }


            var product = _kernel.GetEntity<eShop.ProductEntities.Entities.Product>(externalId: Guid.Parse(request.ProductId));

            if (product.IsNull())
            {
                throw new Exception("Invalid product");
            }
            else
            {
                var basketProduct = _kernel.GetEntities<eShop.ProductEntities.Entities.Basket>()
                    .FirstOrDefault(basket => basket.ProductId == product.Id && basket.CreatedById == _audience.EntityId());
                _kernel.DeleteEntity(basketProduct, saveChanges: true);
            }

            return Task.FromResult(new BasketResponse
            {
                ProductId = product.ExternalId.ToString()
            });
        }
    }
}