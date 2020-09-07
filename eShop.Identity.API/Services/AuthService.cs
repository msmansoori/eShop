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
            user.PersonalEmail.ToLower() == request.Email.ToLower().Trim());

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
            token.Token == request.Token.Trim());

            User user = null;
            if (!userToken.IsNull())
            {
                user = _kernel.GetEntity<User>(externalId: Guid.Parse(userToken.Token));
            }

            return Task.FromResult(new TokenResponse
            {
                Id = user.IsNull() ? 0 : user.Id,
                UserType = user.UserType.ToString(),
                IsValid = !userToken.IsNull()
            });
        }

        public override Task<LogoutResponse> Logout(LogoutRequest request, ServerCallContext context)
        {
            if (request.Token.IsEmpty())
            {
                throw new Exception("please enter token");
            }

            var userTokens = _kernel.GetEntities<UserToken>().Where(token => token.Token == request.Token.Trim());

            foreach (var userToken in userTokens)
            {
                userToken.Active = false;
            }

            var entity = _kernel.UpdateMultiple(entities: userTokens, saveChanges: true);
            return Task.FromResult(new LogoutResponse { });
        }

        private string GenerateToken(User user)
        {
            var token = _kernel.AddEntity(new UserToken { Token = user.ExternalId.ToString(), ExpiryDate = DateTime.UtcNow.AddMinutes(10) }, saveChanges: true);
            return token.Token;
        }
    }
}
