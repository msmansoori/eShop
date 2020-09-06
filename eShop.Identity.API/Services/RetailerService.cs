using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Common.Utilities;
using eShop.IdentityAPI;
using eShop.IdentityEntities.Entities;
using eShop.IdentityEntities.Entities.Enums;
using eShop.InternalServer.Interfaces;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace eShop.Identity.API
{
    public class RetailerService : Retailer.RetailerBase
    {
        private readonly ILogger<RetailerService> _logger;
        private readonly IMicroKernel _kernel;
        public RetailerService(ILogger<RetailerService> logger, IMicroKernel kernel)
        {
            _logger = logger;
            _kernel = kernel;
        }

        public override Task<PaginatedRetailerResponse> GetRetailers(RetailerItemsRequest request, ServerCallContext context)
        {
            var result = _kernel.GetEntities<User>().Where(user => user.UserType == UserType.Retailer);

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                result = result.Where(user => user.Name.Contains(request.Search));
            }

            var itemsOnPage = result
                .OrderBy(c => c.ModifiedOn).ThenBy(c => c.ModifiedOn)
                .Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToList();
            var model = this.MapToResponse(itemsOnPage, result.Count(), request.PageIndex, request.PageSize);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(model);
        }

        public override Task<RetailerResponse> GetRetailer(RetailerItemRequest request, ServerCallContext context)
        {
            var user = _kernel.GetEntity<User>(externalId: ParseUserID(id: request.Id));
            ValidateUserType(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }


        public override Task<RetailerResponse> AddRetailer(RetailerRequest request, ServerCallContext context)
        {
            if (request.PersonalEmail.IsEmpty() || request.Password.IsEmpty())
            {
                throw new Exception("Email and password are required");
            }
            var user = _kernel.GetEntities<User>().FirstOrDefault(user => user.PersonalEmail.ToLower() == request.PersonalEmail.Trim().ToLower());
            if (!user.IsNull())
            {
                throw new Exception("Email address already exist");
            }

            user = new User
            {
                PersonalEmail = request.PersonalEmail,
                BusinessEmail = request.BusinessEmail,
                City = request.City,
                ContactNumber = request.ContactNumber,
                Line1 = request.Line1,
                Line2 = request.Line2,
                Name = request.Name,
                Password = request.Password,
                State = request.State,
                UserType = UserType.Retailer,
                Zipcode = request.Zipcode
            };
            _kernel.AddEntity(entity: user, saveChanges: true);
            ValidateUserType(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }

        public override Task<RetailerResponse> UpdateRetailer(RetailerUpdateRequest request, ServerCallContext context)
        {
            var user = _kernel.GetEntity<User>(externalId: ParseUserID(id: request.Id));
            ValidateUserType(user);

            user.PersonalEmail = request.PersonalEmail;
            user.BusinessEmail = request.BusinessEmail;
            user.City = request.City;
            user.ContactNumber = request.ContactNumber;
            user.ModifiedOn = DateTime.UtcNow;
            user.Line1 = request.Line1;
            user.Line2 = request.Line2;
            user.Name = request.Name;
            user.State = request.State;
            user.Zipcode = request.Zipcode;

            _kernel.UpdateEntity(entity: user, saveChanges: true);
            ValidateUserType(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }

        public override Task<RetailerResponse> DeleteRetailer(RetailerItemRequest request, ServerCallContext context)
        {
            var user = _kernel.DeleteEntity<User>(externalId: ParseUserID(id: request.Id));
            ValidateUserType(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }

        private PaginatedRetailerResponse MapToResponse(List<eShop.IdentityEntities.Entities.User> items, long count, int pageIndex, int pageSize)
        {
            var result = new PaginatedRetailerResponse()
            {
                Count = count,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            items.ForEach(i =>
            {
                result.Data.Add(new RetailerListResponse()
                {
                    Id = i.ExternalId.ToString(),
                    Name = i.Name,
                    BusinessEmail = i.BusinessEmail,
                    PersonalEmail = i.PersonalEmail,
                    Address = i.FullAddress
                });
            });
            return result;
        }

        private RetailerResponse MapToResponse(eShop.IdentityEntities.Entities.User user)
        {
            return new RetailerResponse
            {
                Id = user.ExternalId.ToString(),
                Name = user.Name,
                PersonalEmail = user.PersonalEmail,
                BusinessEmail = user.BusinessEmail,
                ContactNumber = user.ContactNumber,
                Line1 = user.Line1,
                Line2 = user.Line2,
                City = user.City,
                State = user.State,
                Zipcode = user.Zipcode,
                FullAddress = user.FullAddress
            };
        }

        private void ValidateUserType(eShop.IdentityEntities.Entities.User user)
        {
            if (user.IsNull() || user.UserType != UserType.Retailer)
            {
                throw new Exception("Invalid retailer");
            }
        }

        private Guid ParseUserID(string id)
        {
            Guid external = Guid.Empty;
            try
            {
                external = Guid.Parse(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid retailer id");
            }
            return external;
        }
    }
}
