using DotNetCore.CAP;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Sales.Contracts;

namespace Sales
{
    public static class Endpoints
    {
        public static void MapSales(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/sales/orders", async context =>
            {
                var publisher = context.RequestServices.GetService<ICapPublisher>();
                await publisher.PublishAsync(nameof(OrderPlaced), new OrderPlaced
                {
                    OrderId = System.Guid.NewGuid()
                });
                await context.Response.WriteAsync("Order has been placed!");
            });
        }
    }
}
