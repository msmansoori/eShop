using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eShop.Common.Constants;
using eShop.Common.Services;
using eShop.IdentityAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eShop.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpPost]
        public async Task<CustomerResponse> AddCustomer([Required] CustomerRequest customerRequest)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Customer.CustomerClient(channel);
                _logger.LogDebug("Grpc get customer request {@request}", customerRequest);
                return await client.AddCustomerAsync(customerRequest);
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
                return await client.UpdateCustomerAsync(customerRequest);
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
                return await client.DeleteCustomerAsync(new CustomerItemRequest { Id = id.ToString() });
            });
            return response;
        }
    }
}
