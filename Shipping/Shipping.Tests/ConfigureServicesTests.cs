using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Shipping.Tests
{
    public class ConfigureServicesTests
    {
        [Fact]
        public void Test1()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(); // just for testing
            serviceCollection.AddShipping();

            var provider = serviceCollection.BuildServiceProvider();
            var createShippingLabel = provider.GetService<ICreateShippingLabel>();
            createShippingLabel.Should().NotBeNull();
        }
    }
}
