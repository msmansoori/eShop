using eShop.Frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShop.Frontend.Components
{
    public class ProductsViewComponent : ViewComponent
    {

        public ProductsViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = new List<Product>();
            try
            {
                var url = "Product";
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:6001/")
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseString = await client.GetStringAsync(url);
                products = JsonConvert.DeserializeObject<List<Product>>(responseString);
            }
            catch (Exception ex)
            {
            }
            return View(products);
        }

        //private Task<List<TodoItem>> GetItemsAsync(int maxPriority, bool isDone)
        //https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-2.1
    }
}
