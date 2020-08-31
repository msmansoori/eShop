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
    public class MegaSaleViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sale = new MegaSale();
            try
            {
                var url = $"Product/MegaSale";
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:6001/")
                };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseString = await client.GetStringAsync(url);
                sale = JsonConvert.DeserializeObject<MegaSale>(responseString);
            }
            catch (Exception ex)
            {
            }
            return View(sale);
        }
    }
}
