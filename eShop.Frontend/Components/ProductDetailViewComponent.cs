using eShop.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShop.Frontend.Components
{
    public class ProductDetailViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(Guid externalId)
        {
            var product = new Product();
            try
            {
                var url = $"Product/{externalId}";
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:6001/")
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseString = await client.GetStringAsync(url);
                product = JsonConvert.DeserializeObject<Product>(responseString);
            }
            catch (Exception ex)
            {
            }
            return View(product);
        }
    }
}
