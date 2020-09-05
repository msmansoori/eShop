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
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly IdentityContext db;
        public CustomerService(ILogger<CustomerService> logger, IdentityContext context)
        {
            db = context;
            _logger = logger;
        }

        public override Task<PaginatedCustomerResponse> GetCustomers(CustomerItemsRequest request, ServerCallContext context)
        {
            var result = db.Users.Where(user => user.Active && user.UserType == UserType.Customer).AsQueryable();

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

        public override Task<CustomerResponse> GetCustomer(CustomerItemRequest request, ServerCallContext context)
        {
            Guid external = Guid.Empty;
            try
            {
                external = Guid.Parse(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid customer id");
            }

            var user = db.Users.FirstOrDefault(user => user.Active && user.UserType == UserType.Customer && user.ExternalId == external);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }

        public override Task<CustomerResponse> AddCustomer(CustomerRequest request, ServerCallContext context)
        {
            if (request.Email.IsEmpty() || request.Password.IsEmpty())
            {
                throw new Exception("Email and password are required");
            }
            var user = db.Users.FirstOrDefault(user => user.Active && user.PersonalEmail.Equals(request.Email, StringComparison.OrdinalIgnoreCase));

            if (!user.IsNull())
            {
                throw new Exception("Email address already exist");
            }

            user = new User
            {
                Active = true,
                PersonalEmail = request.Email,
                City = request.City,
                ContactNumber = request.ContactNumber,
                CreatedOn = DateTime.UtcNow,
                ExternalId = Guid.NewGuid(),
                Line1 = request.Line1,
                Line2 = request.Line2,
                Name = request.Name,
                Password = request.Password,
                State = request.State,
                UserType = UserType.Customer,
                Zipcode = request.Zipcode
            };
            db.Users.Add(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }

        public override Task<CustomerResponse> UpdateCustomer(CustomerUpdateRequest request, ServerCallContext context)
        {

            Guid external = Guid.Empty;
            try
            {
                external = Guid.Parse(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid customer");
            }

            var user = db.Users.FirstOrDefault(user => user.Active && user.UserType == UserType.Customer && user.ExternalId == external);

            if (user.IsNull())
            {
                throw new Exception("Invalid customer");
            }

            user.PersonalEmail = request.Email;
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


        public override Task<CustomerResponse> DeleteCustomer(CustomerItemRequest request, ServerCallContext context)
        {
            Guid external = Guid.Empty;
            try
            {
                external = Guid.Parse(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid customer id");
            }

            var user = db.Users.FirstOrDefault(user => user.Active && user.UserType == UserType.Customer && user.ExternalId == external);
            var mappedUser = MapToResponse(user: user);
            user.Active = false;
            db.Users.Update(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(mappedUser);
        }
        private PaginatedCustomerResponse MapToResponse(List<eShop.IdentityEntities.Entities.User> items, long count, int pageIndex, int pageSize)
        {
            var result = new PaginatedCustomerResponse()
            {
                Count = count,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            items.ForEach(i =>
            {
                result.Data.Add(new CustomerListResponse()
                {
                    Id = i.ExternalId.ToString(),
                    Name = i.Name,
                    Email = i.PersonalEmail,
                    Address = i.FullAddress
                });
            });
            return result;
        }

        private CustomerResponse MapToResponse(eShop.IdentityEntities.Entities.User user)
        {
            if (user.IsNull())
            {
                throw new Exception("Invalid customer");
            }
            return new CustomerResponse
            {
                Id = user.ExternalId.ToString(),
                Name = user.Name,
                Email = user.PersonalEmail,
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
