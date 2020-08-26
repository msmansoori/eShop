using eShop.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eShop.Frontend.Components
{
    public class CategorySectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
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

            return View(categories);
        }
    }
}
