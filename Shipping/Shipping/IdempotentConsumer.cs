namespace Shipping
{
    public class IdempotentConsumer
    {
        public string Consumer { get; init; }
        public long MessageId { get; init; }
    }
}
