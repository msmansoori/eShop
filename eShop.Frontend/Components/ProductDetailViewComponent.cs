using Microsoft.AspNetCore.Mvc;
using System;

namespace eShop.Frontend.Components
{
    public class ProductDetailViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Guid externalId)
        {
            return View();
        }
    }
}
