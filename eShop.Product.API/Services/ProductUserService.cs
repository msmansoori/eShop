using eShop.Common.Enums;
using eShop.Common.Utilities;
using eShop.InternalServer.Interfaces;
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
        private readonly IMicroKernel _kernel;
        public ProductUserService(ILogger<ProductUserService> logger, IMicroKernel kernel)
        {
            _logger = logger;
            _kernel = kernel;
        }

        public override Task<ProductUserResponse> UpdateRetailer(ProductUserRequest request, ServerCallContext context)
        {
            var retailer = AddUpdateUser(name: request.Name, referenceId: Guid.Parse(request.Id), type: UserType.Retailer);

            return Task.FromResult(new ProductUserResponse
            {
                Name = retailer.Name,
                Id = retailer.ExternalId.ToString()
            });
        }


        public override Task<ProductUserResponse> UpdateCustomer(ProductUserRequest request, ServerCallContext context)
        {
            var customer = AddUpdateUser(name: request.Name, referenceId: Guid.Parse(request.Id), type: UserType.Customer);

            return Task.FromResult(new ProductUserResponse
            {
                Name = customer.Name,
                Id = customer.ExternalId.ToString()
            });
        }


        private User AddUpdateUser(string name, Guid referenceId, UserType type)
        {
            var user = _kernel.GetEntities<User>().FirstOrDefault(user => user.InternalReference == referenceId);
            if (user.IsNull())
            {
                user = new User { Name = name, InternalReference = referenceId, UserType = type };
                _kernel.AddEntity(entity: user);
            }
            else
            {
                user.Name = name;
                _kernel.UpdateEntity(entity: user);
            }
            _kernel.SaveChanges();
            return user;
        }
    }
}
