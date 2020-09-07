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
    public class ProductUserService : ProductUser.ProductUserBase
    {
        private readonly ILogger<ProductUserService> _logger;
        private readonly ProductContext db;

        public ProductUserService(ILogger<ProductUserService> logger, ProductContext context)
        {
            db = context;
            _logger = logger;
        }

        public override Task<ProductUserResponse> UpdateRetailer(ProductUserRequest request, ServerCallContext context)
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

            return Task.FromResult(new ProductUserResponse
            {
                Name = retailer.Name,
                Id = retailer.ExternalId.ToString()
            });
        }
    }
}
