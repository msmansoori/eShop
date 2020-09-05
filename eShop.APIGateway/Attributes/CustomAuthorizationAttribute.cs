using eShop.Common.Constants;
using eShop.Common.Services;
using eShop.IdentityAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eShop.APIGateway.Attributes
{

    public sealed class CustomAuthorizationAttribute : Attribute, IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            try
            {
                var skipAuthorization = (filterContext.ActionDescriptor as ControllerActionDescriptor)
                       .MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);
                if (skipAuthorization.Length > 0)
                {
                    return;
                }
                var tokenString = filterContext.HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrWhiteSpace(tokenString))
                {
                    throw new UnauthorizedAccessException();
                }

                var response = Task.Run(async () =>
                await GrpcCallerService.CallService(urlGrpc: GRPCUrl.IdentityService, logger: null,
                func: async channel =>
                     {
                         var client = new Auth.AuthClient(channel);
                         return await client.ValidTokenAsync(new TokenRequest { Token = tokenString });
                     }))
                    .ConfigureAwait(false).GetAwaiter().GetResult();

                if (!response.IsValid)
                {
                    throw new UnauthorizedAccessException();
                }

                SetClaim(filterContext: filterContext, user: tokenString);
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException();
            }
        }
        private void SetClaim(AuthorizationFilterContext filterContext, string user)
        {
            var ci = (ClaimsIdentity)filterContext.HttpContext.User.Identity;
            ci.AddClaim(new Claim("CurrentUser", user));
        }
    }
}
