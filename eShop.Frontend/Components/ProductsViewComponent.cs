using eShop.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eShop.Frontend.Components
{
    public class ProductsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var products = new List<Product>()
            {
                new Product {
                    Name="Buttons tweed blazer",
                    Category="women",
                    Image="/img/product/product-1.jpg",
                    IsNewArrival=true,
                    Rating=5,
                    Price=560.56
                },
                 new Product {
                    Name="Flowy striped skirt",
                    Category="men",
                    Image="/img/product/product-2.jpg",
                    Rating=4,
                    Price=49.0
                },
                  new Product {
                    Name="Cotton T-Shirt",
                    Category="accessories",
                    Image="/img/product/product-3.jpg",
                    IsOutOfStock=true,
                    Rating=5,
                    Price=43.59
                },
                   new Product {
                    Name="Slim striped pocket shirt",
                    Category="cosmetic",
                    Image="/img/product/product-4.jpg",
                    Rating=3,
                    Price=76.25
                },
                    new Product {
                    Name="Fit micro corduroy shirt",
                    Category="kid",
                    Image="/img/product/product-5.jpg",
                    Rating=5,
                    Price=46.58
                },
                     new Product {
                    Name="Tropical Kimono",
                    Category="women men kid accessories cosmetic",
                    Image="/img/product/product-6.jpg",
                    Rating=4,
                    Price=35.46,
                    HasDiscount=true,
                    Discount=5
                },
                      new Product {
                    Name="Contrasting sunglasses",
                    Category="kid accessories cosmeti",
                    Image="/img/product/product-7.jpg",
                    Rating=5,
                    Price=76
                },
                       new Product {
                    Name="Water resistant backpack",
                    Category="women men kid accessories cosmetic",
                    Image="/img/product/product-8.jpg",
                    IsNewArrival=true,
                    Rating=5,
                    Price=80.11,
                    HasDiscount=true,
                    Discount=10.11
                }
            };
            return View(products);
        }
    }
}
