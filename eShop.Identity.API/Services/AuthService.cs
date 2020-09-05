using System;
using System.Linq;
using System.Threading.Tasks;
using eShop.Common.Utilities;
using eShop.IdentityAPI;
using eShop.IdentityEntities.Entities;
using eShop.InternalServer.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace eShop.Identity.API
{
    public class AuthService : Auth.AuthBase
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IMicroKernel _kernel;
        public AuthService(ILogger<AuthService> logger, IMicroKernel kernel)
        {
            _logger = logger;
            _kernel = kernel;
        }

        public override Task<LoginResponse> HandleLogin(LoginRequest request, ServerCallContext context)
        {
            if (request.Email.IsEmpty())
            {
                throw new Exception("please enter email address");
            }

            if (request.Password.IsEmpty())
            {
                throw new Exception("please enter password");
            }

            var user = _kernel.GetEntities<User>().FirstOrDefault(user => user.PersonalEmail != null &&
            user.PersonalEmail.Equals(request.Email.Trim(), StringComparison.OrdinalIgnoreCase));

            if (user.IsNull())
            {
                throw new Exception("Invalid email address");
            }
            else if (user.Password != request.Password)
            {
                throw new Exception("Incorrect password");
            }

            return Task.FromResult(new LoginResponse
            {
                FullName = user.Name,
                Token = GenerateToken(user: user)
            });
        }

        public override Task<TokenResponse> ValidToken(TokenRequest request, ServerCallContext context)
        {
            if (request.Token.IsEmpty())
            {
                throw new Exception("please enter token");
            }

            var userToken = _kernel.GetEntities<UserToken>().FirstOrDefault(token => !token.IsExpired && token.ExpiryDate > DateTime.UtcNow &&
            token.Token.Equals(request.Token.Trim(), StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(new TokenResponse
            {
                IsValid = !userToken.IsNull()
            });
        }

        private string GenerateToken(User user)
        {
            var token = _kernel.AddEntity(new UserToken { Token = user.ExternalId.ToString() });
            _kernel.SaveChanges();
            return token.Token;
        }
    }
}
