using Microsoft.Extensions.DependencyInjection;

namespace Shipping
{
    public static class ConfigureServices
    {
        public static void AddShipping(this IServiceCollection service)
        {
            service.AddDbContext<ShippingDbContext>();
        }

        public static void AddShippingCapSubscribe(this IServiceCollection service)
        {
            service.AddShipping();
            service.AddTransient<ICreateShippingLabel, CreateShippingLabel>();
        }
    }
}
