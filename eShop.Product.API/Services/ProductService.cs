using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.Common.Utilities;
using eShop.InternalServer.Interfaces;
using eShop.ProductAPI;
using eShop.ProductEntities.Context;
using eShop.ProductEntities.Entities;
using eShop.ProductEntities.Entities.Enums;
using Google.Protobuf;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static eShop.ProductAPI.Product;

namespace eShop.Product.API.Services
{
    public class ProductService : ProductBase
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IMicroKernel _kernel;
        private readonly IAudience _audience;
        public ProductService(ILogger<ProductService> logger, IMicroKernel kernel, IAudience audience)
        {
            _logger = logger;
            _kernel = kernel;
            _audience = audience;
        }

        public override Task<PaginatedItemsResponse> GetProducts(ProductItemsRequest request, ServerCallContext context)
        {
            var result = _kernel.GetEntities<eShop.ProductEntities.Entities.Product>();

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

        public override Task<ProductResponse> GetProductById(ProductItemRequest request, ServerCallContext context)
        {
            var product = _kernel.GetEntity<eShop.ProductEntities.Entities.Product>(externalId: ParseUserID(id: request.Id));
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(product: product));
        }


        public override Task<ProductResponse> AddProduct(ProductRequest request, ServerCallContext context)
        {
            UserValidation();

            var brand = GetBrand(request.Brand);
            var product = new eShop.ProductEntities.Entities.Product
            {
                AvailableStock = Convert.ToInt32(request.AvailableStock),
                Brand = brand
            };

            _kernel.AddEntity(entity: product, saveChanges: true);
            context.Status = new Status(StatusCode.OK, string.Empty);
            return Task.FromResult(MapToResponse(product: product));
        }


        public override Task<ProductResponse> UpdateProduct(ProductRequest request, ServerCallContext context)
        {
            return base.UpdateProduct(request, context);
        }

        public override Task<ProductResponse> DeleteProduct(ProductItemRequest request, ServerCallContext context)
        {
            return base.DeleteProduct(request, context);
        }

        #region private functions
        private PaginatedItemsResponse MapToResponse(List<eShop.ProductEntities.Entities.Product> items, long count, int pageIndex, int pageSize)
        {
            var result = new PaginatedItemsResponse()
            {
                Count = count,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };

            items.ForEach(i =>
            {
                result.Data.Add(new ProductListResponse
                {
                    Id = i.ExternalId.ToString(),
                    Name = i.Name,
                    Category = GetCategory(i),
                    Brand = GetBrand(i),
                    Price = (double)i.Price,
                    Discount = i.Discount,
                    DiscountPrice = (double)i.DiscountPrice,
                    AvailableStock = i.AvailableStock
                });
            });
            return result;
        }


        private ProductBrand GetBrand(eShop.ProductEntities.Entities.Product product)
        {
            return product.Brand != null && product.Brand.Active ? new ProductBrand { Id = product.Brand.ExternalId.ToString(), Name = product.Brand.Display } : null;
        }

        private ProductType GetCategory(eShop.ProductEntities.Entities.Product product)
        {
            return product.Category != null && product.Category.Active ? new ProductType { Id = product.Category.ExternalId.ToString(), Name = product.Category.Display } : null;
        }

        private ProductResponse MapToResponse(eShop.ProductEntities.Entities.Product product)
        {
            return new ProductResponse
            {
                Id = product.ExternalId.ToString(),
                Name = product.Display,
                Category = GetCategory(product),
                Brand = GetBrand(product),
                Price = (double)product.Price,
                Discount = product.Discount,
                DiscountPrice = (double)product.DiscountPrice,
                AvailableStock = product.AvailableStock,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Specification = product.Specification,
                Colors = GetEmunStings<Color>(product.Colors),
                Sizes = GetEmunStings<Size>(product.Sizes),
                Promotions = product.Promotion.ToString()
            };
        }

        private string GetEmunStings<T>(string enumStr) where T : struct
        {
            var enumTypes = new List<string>();
            if (!string.IsNullOrWhiteSpace(enumStr))
            {
                T enumType;
                foreach (var item in enumStr.Split(";"))
                {
                    Enum.TryParse<T>(item, out enumType);
                    enumTypes.Add(enumType.ToString());
                }
            }
            return string.Join(";", enumTypes);
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
                throw new Exception("Invalid product id");
            }
            return external;
        }


        private void UserValidation()
        {
            var userType = _audience.UserType();
            if (userType != Common.Enums.UserType.Retailer)
            {
                throw new Exception("This user can not add/update products");
            }
        }


        private Brand GetBrand(eShop.ProductAPI.ProductBrand brandDto)
        {
            if (brandDto.IsNull())
            {
                return null;
            }

            var brand = _kernel.GetEntity<Brand>(externalId: ParseUserID(brandDto.Id));

            if (brand.IsNull())
            {
                brand = new Brand
                {
                    Name = brandDto.Name
                };
                _kernel.AddEntity(entity: brand, saveChanges: true);
            }
            else if (!brandDto.Name.Equals(brand.Name, StringComparison.OrdinalIgnoreCase))
            {
                brand.Name = brandDto.Name;
                _kernel.UpdateEntity(entity: brand, saveChanges: true);
            }

            return brand;
        }

        #endregion private functions

    }
}
