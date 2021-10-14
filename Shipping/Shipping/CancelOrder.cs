using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Shipping
{
    public class CancelOrder
    {
        private readonly ShippingDbContext _dbContext;

        public CancelOrder(ShippingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("/shipping/order/cancel/{orderId:Guid}")]
        public async Task Cancel(Guid orderId)
        {
            var order = await _dbContext.ShippingLabels.SingleAsync(x => x.OrderId == orderId);
            order.Candelled = true;
            await _dbContext.SaveChangesAsync();
        }
    }
}
