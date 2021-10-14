using Microsoft.Extensions.DependencyInjection;

namespace Sales
{
    public static class ConfigureServices
    {
        public static void AddSales(this IServiceCollection service)
        {
            service.AddDbContext<SalesDbContext>();
        }

        public static void AddSalesCapSubscribe(this IServiceCollection service)
        {
            service.AddSales();
            service.AddTransient<IChangeOrderStatus, ChangeOrderStatus>();
        }
    }
}
