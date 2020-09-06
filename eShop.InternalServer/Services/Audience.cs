
using System;
using System.Security.Claims;
using eShop.InternalServer.Interfaces;
using Microsoft.AspNetCore.Http;

namespace eShop.InternalServer.Services
{
    public class Audience : IAudience
    {
        private readonly IHttpContextAccessor accessor;
        public Audience(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public Guid GetCurrentUser()
        {
            var externalId = Guid.Empty;
            var claimsIdentity = accessor.HttpContext.User?.Identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                // get some claim by type
                var someClaim = claimsIdentity.FindFirst("CurrentUser")?.Value;
                Guid.TryParse(someClaim, out externalId);
            }
            return externalId;
        }
    }
}
