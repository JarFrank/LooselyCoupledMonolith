using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Sales.Contracts;
using Shipping.Contracts;
using System.Threading.Tasks;

namespace Shipping
{
    public interface ICreateShippingLabel
    {
        Task Handle(OrderPlaced orderPlaced, [FromCap] CapHeader header);
    }

    public class CreateShippingLabel : ICreateShippingLabel, ICapSubscribe
    {
        private readonly ILogger<CreateShippingLabel> _logger;
        private readonly ICapPublisher _publisher;
        private readonly ShippingDbContext _dbContext;

        public CreateShippingLabel(ILogger<CreateShippingLabel> logger, ICapPublisher publisher, ShippingDbContext dbContext)
        {
            _logger = logger;
            _publisher = publisher;
            _dbContext = dbContext;
        }

        [CapSubscribe(nameof(OrderPlaced))]
        public async Task Handle(OrderPlaced orderPlaced, [FromCap] CapHeader header)
        {
            _logger.LogInformation($"Order {orderPlaced.OrderId} has been created a shipping label.");

            var messageId = header.GetMessageId();
            _logger.LogInformation($"MessageId: {messageId}");
            if (await _dbContext.HasBeenProcessed(messageId, nameof(ShippingLabel)))
            {
                return;
            }

            using var trx = _dbContext.Database.BeginTransaction();
            await _dbContext.ShippingLabels.AddAsync(new ShippingLabel
            {
                OrderId = orderPlaced.OrderId,
                OrderDate = System.DateTime.UtcNow,
            });
            await _dbContext.SaveChangesAsync();

            await _publisher.PublishAsync(nameof(ShippingLabelCreated), new ShippingLabelCreated
            {
                OrderId = orderPlaced.OrderId
            });
            await _dbContext.IdempotentConsumer(messageId, nameof(ShippingLabel));

            await trx.CommitAsync();
        }
    }
}
