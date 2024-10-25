using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GatewayController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var client = _httpClientFactory.CreateClient(); // Create a new client instance
            var response = await client.GetAsync("http://orderservice:80/api/orders"); // Change the path as per your API
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var client = _httpClientFactory.CreateClient(); // Create a new client instance
            var response = await client.GetAsync("http://productservice:80/api/products"); // Change the path as per your API
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }
    }
}
