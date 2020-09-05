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
    public class RetailerController : ControllerBase
    {
        private readonly ILogger<RetailerController> _logger;

        public RetailerController(ILogger<RetailerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<PaginatedRetailerResponse> Get([Required] RetailerItemsRequest request)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid model");
            }
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Retailer.RetailerClient(channel);
                _logger.LogDebug("Grpc get Retailer request {@request}", request);
                return await client.GetRetailersAsync(request);
            });
            return response;
        }

        [HttpGet, Route("{id}")]
        public async Task<RetailerResponse> Get([Required] Guid id)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Retailer.RetailerClient(channel);
                _logger.LogDebug("Grpc get Retailer request {@request}", id);
                return await client.GetRetailerAsync(new RetailerItemRequest { Id = id.ToString() });
            });
            return response;
        }

        [HttpPost]
        public async Task<RetailerResponse> AddRetailer([Required] RetailerRequest request)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Retailer.RetailerClient(channel);
                _logger.LogDebug("Grpc post Retailer request {@request}", request);
                var retailer = await client.AddRetailerAsync(request);
                await UpdateProductRetailer(retailer);
                return retailer;
            });
            return response;
        }

        [HttpPut]
        public async Task<RetailerResponse> UpdateRetailer([Required] RetailerUpdateRequest request)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Retailer.RetailerClient(channel);
                _logger.LogDebug("Grpc put Retailer request {@request}", request);
                var retailer = await client.UpdateRetailerAsync(request);
                await UpdateProductRetailer(retailer);
                return retailer;
            });
            return response;
        }

        [HttpDelete, Route("{id}")]
        public async Task<RetailerResponse> DeleteCustomer([Required] Guid id)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: _logger, func: async channel =>
            {
                var client = new Retailer.RetailerClient(channel);
                _logger.LogDebug("Grpc delete customer request {@request}", id);
                var retailer = await client.DeleteRetailerAsync(new RetailerItemRequest { Id = id.ToString() });
                await UpdateProductRetailer(retailer, true);
                return retailer;
            });
            return response;
        }

        private async Task<ProductRetailerResponse> UpdateProductRetailer(RetailerResponse retailer, bool isDeleted = false)
        {
            var response = await GrpcCallerService.CallService(urlGrpc: GRPCUrl.ProductService, logger: _logger, func: async channel =>
            {
                var client = new ProductRetailer.ProductRetailerClient(channel);
                _logger.LogDebug("Grpc delete customer request {@request}", retailer);
                return await client.UpdateRetailerAsync(new ProductRetailerUpdateRequest { Id = retailer.Id, Name = retailer.Name, IsDeleted = isDeleted });
            });
            return response;
        }
    }
}
