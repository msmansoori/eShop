using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using eShop.APIGateway.Attributes;
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
    [CustomAuthorization]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<PaginatedCustomerResponse> Get([Required] CustomerItemsRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model");
            }
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Customer.CustomerClient(channel);
                _logger.LogDebug("Grpc get customer request request {@request}", request);
                return await client.GetCustomersAsync(request);
            });
            return response;
        }

        [HttpGet, Route("{id}")]
        public async Task<CustomerResponse> Get([Required] Guid id)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Customer.CustomerClient(channel);
                _logger.LogDebug("Grpc get customer request request {@request}", id);
                return await client.GetCustomerAsync(new CustomerItemRequest { Id = id.ToString() });
            });
            return response;
        }

        [HttpPut]
        public async Task<CustomerResponse> UpdateCustomer([Required] CustomerUpdateRequest customerRequest)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Customer.CustomerClient(channel);
                _logger.LogDebug("Grpc get customer request {@request}", customerRequest);
                var customer = await client.UpdateCustomerAsync(customerRequest);
                await UpdateProductCustomer(customer, false);
                return customer;
            });
            return response;
        }

        [HttpDelete, Route("{id}")]
        public async Task<CustomerResponse> DeleteCustomer([Required] Guid id)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Customer.CustomerClient(channel);
                _logger.LogDebug("Grpc delete customer request {@request}", id);
                var customer = await client.DeleteCustomerAsync(new CustomerItemRequest { Id = id.ToString() });
                await UpdateProductCustomer(customer, true);
                return customer;
            });
            return response;
        }

        private async Task<ProductUserResponse> UpdateProductCustomer(CustomerResponse customer, bool isDeleted = false)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.ProductService, logger: _logger, func: async channel =>
            {
                var client = new ProductUser.ProductUserClient(channel);
                _logger.LogDebug("Grpc update product customer request {@request}", customer);
                return await client.UpdateCustomerAsync(new ProductUserRequest { Id = customer.Id, Name = customer.Name, IsDeleted = isDeleted });
            });
            return response;
        }
    }
}
