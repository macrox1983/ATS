using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ATS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureKestrel((context, options) => 
            {
                var apiUri = new Uri(context.Configuration.GetValue<string>("ApiUri"));
                IPAddress address = IPAddress.Any;
                switch(apiUri.Host.ToLower())
                {
                    case "localhost":
                        address = IPAddress.Loopback;
                        break;
                    case "*":
                        address = IPAddress.Any;
                        break;
                    default:
                        address = IPAddress.Parse(apiUri.Host);
                        break;
                }
                options.Listen(new IPEndPoint(address, apiUri.Port), loptions =>
                {
                });                
            })
            .UseStartup<Startup>();
    }
}
