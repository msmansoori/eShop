using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eShop.Common.Constants;
using eShop.Common.Services;
using eShop.IdentityAPI;
using eShop.ProductAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eShop.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handle user login
        /// </summary>
        /// <remarks>
        /// Email: admin@gmail.com,retailer@gmail.com,customer@gmail.com
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, Route("login")]
        public async Task<LoginResponse> Login([Required] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model");
            }
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Auth.AuthClient(channel);
                _logger.LogDebug("Grpc auth login {@request}", request);
                return await client.HandleLoginAsync(request);
            });
            return response;
        }


        [HttpPost, Route("logout")]
        public async Task<LogoutResponse> Logout([Required] LogoutRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model");
            }
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Auth.AuthClient(channel);
                _logger.LogDebug("Grpc auth logout {@request}", request);
                return await client.LogoutAsync(request);
            });
            return response;
        }


        [HttpPost, Route("register")]
        public async Task<CustomerResponse> Register([Required] CustomerRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model");
            }

            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Customer.CustomerClient(channel);
                _logger.LogDebug("Grpc get customer request {@request}", request);
                var customer = await client.AddCustomerAsync(request);
                var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.ProductService, logger: _logger, func: async channel =>
                {
                    var client = new ProductUser.ProductUserClient(channel);
                    _logger.LogDebug("Grpc add product customer request {@request}", customer);
                    return await client.UpdateCustomerAsync(new ProductUserRequest { Id = customer.Id, Name = customer.Name, IsDeleted = false });
                });
                return customer;
            });

            return response;
        }
    }
}
