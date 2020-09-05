using eShop.Common.Utilities;
using eShop.ProductAPI;
using eShop.ProductEntities.Context;
using eShop.ProductEntities.Entities;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Product.API.Services
{
    public class ProductRetailerService : ProductRetailer.ProductRetailerBase
    {
        private readonly ILogger<ProductRetailerService> _logger;
        private readonly ProductContext db;

        public ProductRetailerService(ILogger<ProductRetailerService> logger, ProductContext context)
        {
            db = context;
            _logger = logger;
        }

        public override Task<ProductRetailerResponse> UpdateRetailer(ProductRetailerUpdateRequest request, ServerCallContext context)
        {
            var retailer = db.Users.FirstOrDefault(user => user.InternalReference.ToString() == request.Id && user.Active);
            if (retailer.IsNull())
            {
                retailer = new User
                {
                    Active = true,
                    Name = request.Name,
                    InternalReference = Guid.Parse(request.Id),
                    CreatedOn = DateTime.UtcNow,
                    ExternalId = Guid.NewGuid()
                };
                db.Users.Add(retailer);
            }
            else
            {
                retailer.ModifiedOn = DateTime.UtcNow;
                retailer.Name = request.Name;
                db.Users.Update(retailer);
            }

            return Task.FromResult(new ProductRetailerResponse
            {
                Name = retailer.Name,
                Id = retailer.ExternalId.ToString()
            });
        }
    }
}
