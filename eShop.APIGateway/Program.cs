using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eShop.APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    /* Note : System.IO.IOException: Failed to bind to address https://localhost:5001.
                    * to resolve this error we have added below code
                    */
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    {
                        // The following statement allows you to call insecure services. To be used only in development environments.
                        AppContext.SetSwitch(
                            "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                    }
                    webBuilder.UseStartup<Startup>();
                });
    }
}
