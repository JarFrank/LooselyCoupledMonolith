using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sales;
using Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSalesCapSubscribe();
                    services.AddShippingCapSubscribe();

                    services.AddCap(options =>
                    {
                        options.UseInMemoryStorage();
                        options.UseRabbitMQ("localhost");
                    });

                    //services.AddHostedService<Worker>();
                });
    }
}
