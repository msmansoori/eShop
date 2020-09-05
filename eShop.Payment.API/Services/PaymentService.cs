using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.PaymentAPI;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace eShop.Payment.API
{
    public class PaymentService : PaymentAPI.Payment.PaymentBase
    {
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(ILogger<PaymentService> logger)
        {
            _logger = logger;
        }

        public override Task<PaymentReply> AddPayment(PaymentRequest request, ServerCallContext context)
        {
            return Task.FromResult(new PaymentReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
