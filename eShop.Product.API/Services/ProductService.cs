using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.ProductAPI;
using eShop.ProductEntities.Context;
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
        private readonly ProductContext db;


        public ProductService(ILogger<ProductService> logger, ProductContext context)
        {
            db = context;
            _logger = logger;
        }

        public override async Task<PaginatedItemsResponse> GetProducts(ProductItemsRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call ProductService.GetProducts for product page index", request.PageIndex);

            var result = db.Products.Where(product => product.Active).AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                result = result.Where(product => product.Display.Contains(request.Search));
            }

            var itemsOnPage = result
                .Include(b => b.Brand)
                .Include(b => b.Category)
                .OrderBy(c => c.ModifiedOn).ThenBy(c => c.ModifiedOn)
                .Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToList();
            var model = this.MapToResponse(itemsOnPage, result.Count(), request.PageIndex, request.PageSize);
            context.Status = new Status(StatusCode.OK, string.Empty);

            return model;
        }


        public override async Task<ProductResponse> GetProductById(ProductItemRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Begin grpc call ProductService.GetProductById for product id", request.Id);

            if (string.IsNullOrWhiteSpace(request.Id))
            {
                _logger.LogInformation("Invalid product", request.Id);
                return null;
            }

            Guid.TryParse(request.Id, out var productId);

            if (Guid.Empty == productId)
            {
                _logger.LogInformation("Invalid product", request.Id);
                return null;
            }


            var result = await db.Products
                .Include(product => product.Brand)
                .Include(product => product.Category)
                .FirstOrDefaultAsync(product => product.Active && product.ExternalId == productId);

            context.Status = new Status(StatusCode.OK, string.Empty);

            //string colors = 12;
            //string sizes = 13;
            //string promotions = 14;
            return new ProductResponse
            {
                Id = result.ExternalId.ToString(),
                Name = result.Display,
                Category = GetCategory(result),
                Brand = GetBrand(result),
                Price = (double)result.Price,
                Discount = result.Discount,
                DiscountPrice = (double)result.DiscountPrice,
                AvailableStock = result.AvailableStock,
                Description = result.Description,
                ShortDescription = result.ShortDescription,
                Specification = result.Specification,
                Colors = GetEmunStings<Color>(result.Colors),
                Sizes = GetEmunStings<Size>(result.Sizes),
                Promotions = result.Promotion.ToString()
            };
        }
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
                result.Data.Add(new ProductListResponse()
                {
                    Id = i.ExternalId.ToString(),
                    Name = i.Name,
                    Category = i.Category != null && i.Category.Active ? new ProductType { Id = i.Category.ExternalId.ToString(), Name = i.Category.Display } : null,
                    Brand = i.Brand != null && i.Brand.Active ? new ProductBrand { Id = i.Brand.ExternalId.ToString(), Name = i.Brand.Display } : null,
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

        private List<ProductImage> GetProductImages(eShop.ProductEntities.Entities.Product product)
        {
            var files = db.UploadedFiles.Where(file => file.Active && file.Entity == Entity.Product && file.EntityId == product.Id);
            var images = new List<ProductImage>();
            foreach (var item in files)
            {
                images.Add(new ProductImage
                {
                    Id = item.ExternalId.ToString(),
                    Name = item.Display,
                    Path = item.FilePath
                });
            }
            return images;
        }

        //private string GetColors(eShop.ProductEntities.Entities.Product product)
        //{
        //    var colors = new List<string>();
        //    if (!string.IsNullOrWhiteSpace(product.Colors))
        //    {
        //        Color color;

        //        foreach (var item in product.Colors.Split(";"))
        //        {
        //            Enum.TryParse<Color>(item, out color);
        //            colors.Add(color.ToString());
        //        }

        //    }
        //    return string.Join(";", colors);
        //}


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
    }
}
