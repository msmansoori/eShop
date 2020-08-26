using eShop.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eShop.Frontend.Components
{
    public class MegaSaleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var sale = new MegaSale
            {
                Name = "Summer 2019",
                Discount = 45,
                Image = "/img/discount.jpg",
                Date = DateTime.Now.AddDays(5)
            };
            return View(sale);
        }
    }
}
