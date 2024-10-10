using EventBus.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Domain.Entities;
using ProductService.Domain.Events;
using ProductService.Domain.Interfaces;
//using ProductService.Infrastructure.RabbitMQ;

namespace ProductService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        //private readonly EventBusRabbitMQ _eventBus;
        private readonly IEventBus _eventBusShared;
        public ProductController(IProductRepository productRepository, IEventBus eventBusShared)
        {
            _productRepository = productRepository;
           // _eventBus = eventBus;
            _eventBusShared = eventBusShared;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            await _productRepository.AddProductAsync(product);

            // Publish an event to RabbitMQ
            var productCreatedEvent = new ProductCreatedEvent
            {
                ProductId = product.ProductId,
                Price = product.Price,
                Description = product.Description,
                Name = product.Name,
            };
           // _eventBus.PublishProductCreatedEvent(productCreatedEvent);
            _eventBusShared.Publish("ProductCreated", productCreatedEvent);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            await _productRepository.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
