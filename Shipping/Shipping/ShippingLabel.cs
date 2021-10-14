using System;

namespace Shipping
{
    public class ShippingLabel
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Candelled { get; set; }
    }
}
