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
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpPost, Route("login")]
        public async Task<LoginResponse> Get([Required] LoginRequest request)
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
    }
}
