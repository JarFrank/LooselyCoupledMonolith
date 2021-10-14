using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Sales.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales
{
    public class PlaceOrder : Controller
    {
        private readonly SalesDbContext _dbContext;
        private readonly ICapPublisher _publisher;

        public PlaceOrder(SalesDbContext dbContext, ICapPublisher publisher)
        {
            _dbContext = dbContext;
            _publisher = publisher;
        }

        [HttpPost("/sales/orders/{orderId:Guid}")]
        public async Task<IActionResult> Action([FromRoute] Guid orderId)
        {
            using var transaction = _dbContext.Database.BeginTransaction();

            await _dbContext.Orders.AddAsync(new Order
            {
                OrderId = orderId,
                Status = OrderStatus.Pending,
            });
            await _dbContext.SaveChangesAsync();

            var orderPlaced = new OrderPlaced
            {
                OrderId = orderId,
            };

            await _publisher.PublishAsync(nameof(OrderPlaced), orderPlaced);

            await transaction.CommitAsync();

            return NoContent();
        }
    }
}
