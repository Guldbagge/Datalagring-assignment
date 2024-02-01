using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Tests
{
    public class ServiceProviderFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public ServiceProviderFixture()
        {
            var services = new ServiceCollection();
            services.AddDbContext<ProductCatalogContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));

            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            // Clean up if needed
        }
    }
}
