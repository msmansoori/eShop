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
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly IMicroKernel _kernel;
        public CustomerService(ILogger<CustomerService> logger, IMicroKernel kernel)
        {
            _logger = logger;
            _kernel = kernel;
        }

        public override Task<PaginatedCustomerResponse> GetCustomers(CustomerItemsRequest request, ServerCallContext context)
        {
            var result = _kernel.GetEntities<User>().Where(user => user.UserType == UserType.Customer);

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
            var user = _kernel.GetEntity<User>(externalId: ParseUserID(id: request.Id));
            ValidateUserType(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }

        public override Task<CustomerResponse> AddCustomer(CustomerRequest request, ServerCallContext context)
        {
            if (request.Email.IsEmpty() || request.Password.IsEmpty())
            {
                throw new Exception("Email and password are required");
            }
            var user = _kernel.GetEntities<User>().FirstOrDefault(user => user.PersonalEmail.ToLower() == request.Email.Trim().ToLower());
            if (!user.IsNull())
            {
                throw new Exception("Email address already exist");
            }

            user = new User
            {
                PersonalEmail = request.Email,
                City = request.City,
                ContactNumber = request.ContactNumber,
                Line1 = request.Line1,
                Line2 = request.Line2,
                Name = request.Name,
                Password = request.Password,
                State = request.State,
                UserType = UserType.Customer,
                Zipcode = request.Zipcode
            };

            _kernel.AddEntity(entity: user, saveChanges: true);
            ValidateUserType(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
        }

        public override Task<CustomerResponse> UpdateCustomer(CustomerUpdateRequest request, ServerCallContext context)
        {
            var user = _kernel.GetEntity<User>(externalId: ParseUserID(id: request.Id));
            ValidateUserType(user);

            user.PersonalEmail = request.Email;
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

        public override Task<CustomerResponse> DeleteCustomer(CustomerItemRequest request, ServerCallContext context)
        {
            var user = _kernel.DeleteEntity<User>(externalId: ParseUserID(id: request.Id));
            ValidateUserType(user);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(user: user));
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

        private void ValidateUserType(eShop.IdentityEntities.Entities.User user)
        {
            if (user.IsNull() || user.UserType != UserType.Customer)
            {
                throw new Exception("Invalid customer");
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
                throw new Exception("Invalid customer id");
            }
            return external;
        }
    }
}
