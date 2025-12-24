using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ProductService.Tests
{
    public class ProductApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        public ProductApiTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Products_ReturnsSuccess()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/products");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<object[]>();
            Assert.NotNull(products);
        }
    }
}
