using eShop.Common.Utilities;
using eShop.IdentityAPI;
using eShop.IdentityEntities.Context;
using eShop.IdentityEntities.Entities;
using eShop.IdentityEntities.Entities.Enums;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Identity.API
{
    public class RetailerService : Retailer.RetailerBase
    {
        private readonly ILogger<RetailerService> _logger;
        private readonly IdentityContext db;
        public RetailerService(ILogger<RetailerService> logger, IdentityContext context)
        {
            db = context;
            _logger = logger;
        }

        public override Task<PaginatedRetailerResponse> GetRetailers(RetailerItemsRequest request, ServerCallContext context)
        {
            var result = db.Users.Where(user => user.Active && user.UserType == UserType.Retailer).AsQueryable();

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
            Guid external = Guid.Empty;
            try
            {
                external = Guid.Parse(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid retailer id");
            }

            var user = db.Users.FirstOrDefault(user => user.Active && user.UserType == UserType.Retailer && user.ExternalId == external);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }


        public override Task<RetailerResponse> AddRetailer(RetailerRequest request, ServerCallContext context)
        {
            if (request.PersonalEmail.IsEmpty() || request.Password.IsEmpty())
            {
                throw new Exception("Email and password are required");
            }
            var user = db.Users.FirstOrDefault(user => user.Active && user.PersonalEmail.Equals(request.PersonalEmail, StringComparison.OrdinalIgnoreCase));

            if (!user.IsNull())
            {
                throw new Exception("Email address already exist");
            }

            user = new User
            {
                Active = true,
                PersonalEmail = request.PersonalEmail,
                BusinessEmail = request.BusinessEmail,
                City = request.City,
                ContactNumber = request.ContactNumber,
                CreatedOn = DateTime.UtcNow,
                ExternalId = Guid.NewGuid(),
                Line1 = request.Line1,
                Line2 = request.Line2,
                Name = request.Name,
                Password = request.Password,
                State = request.State,
                UserType = UserType.Retailer,
                Zipcode = request.Zipcode
            };
            db.Users.Add(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }

        public override Task<RetailerResponse> UpdateRetailer(RetailerUpdateRequest request, ServerCallContext context)
        {
            Guid external = Guid.Empty;
            try
            {
                external = Guid.Parse(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid retailer");
            }

            var user = db.Users.FirstOrDefault(user => user.Active && user.UserType == UserType.Customer && user.ExternalId == external);

            if (user.IsNull())
            {
                throw new Exception("Invalid retailer");
            }

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

            db.Users.Update(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }

        public override Task<RetailerResponse> DeleteRetailer(RetailerItemRequest request, ServerCallContext context)
        {
            Guid external = Guid.Empty;
            try
            {
                external = Guid.Parse(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid retailer id");
            }

            var user = db.Users.FirstOrDefault(user => user.Active && user.UserType == UserType.Retailer && user.ExternalId == external);
            var mappedUser = MapToResponse(user: user);
            user.Active = false;
            db.Users.Update(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(mappedUser);
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
            if (user.IsNull())
            {
                throw new Exception("Invalid retailer");
            }
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
    }
}
