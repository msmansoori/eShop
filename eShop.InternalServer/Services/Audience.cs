
using System;
using System.Security.Claims;
using eShop.Common.Enums;
using eShop.Common.Utilities;
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

        public Guid ExternalId()
        {
            var id = Guid.Empty;
            var claimsIdentity = accessor.HttpContext.User?.Identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                // get some claim by type
                var externalId = claimsIdentity.FindFirst("externalId")?.Value;
                Guid.TryParse(externalId, out id);
            }
            return id;
        }

        public long EntityId()
        {
            long id = 0;
            var claimsIdentity = accessor.HttpContext.User?.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // get some claim by type
                var entityId = claimsIdentity.FindFirst("entityId")?.Value;
                long.TryParse(entityId, out id);
            }
            return id;
        }

        public UserType? UserType()
        {
            var claimsIdentity = accessor.HttpContext.User?.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // get some claim by type
                var type = claimsIdentity.FindFirst("userType")?.Value;
                if (type.IsNotEmpty())
                {
                    Enum.TryParse<UserType>(type, out UserType userType);
                    return userType;
                }
            }
            return null;
        }
    }
}
