using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.APIGateway.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eShop.APIGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ILogger<ProductCategoryController> _logger;

        public ProductCategoryController(ILogger<ProductCategoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet,Route("Sections")]
        public IEnumerable<CategorySection> GetCategorySections()
        {
            var categories = new List<CategorySection>
            {
                new CategorySection
                {
                    Name="Women’s fashion",
                    ProductCount=408,
                    Image="/img/categories/category-1.jpg"
                },
                 new CategorySection
                {
                    Name="Men’s fashion",
                    ProductCount=15258,
                    Image="/img/categories/category-2.jpg"
                },
                  new CategorySection
                {
                    Name="Kid’s fashion",
                    ProductCount=273,
                    Image="/img/categories/category-3.jpg"
                },
                   new CategorySection
                {
                    Name="Cosmetics",
                    ProductCount=159,
                    Image="/img/categories/category-4.jpg"
                },
                   new CategorySection
                {
                    Name="Accessories",
                    ProductCount=792,
                    Image="/img/categories/category-5.jpg"
                }
            };
            return categories;
        }
    }
}
