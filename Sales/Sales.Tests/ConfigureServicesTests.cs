using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Sales.Tests
{
    public class ConfigureServicesTests
    {
        [Fact]
        public void SalesDbContext_should_be_not_null()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSales();

            var provider = serviceCollection.BuildServiceProvider();
            var dbContext = provider.GetService<SalesDbContext>();
            dbContext.Should().NotBeNull();
        }
    }
}
