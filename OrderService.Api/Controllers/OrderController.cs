using EventBus.Interfaces;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Entities;
using OrderService.Domain.Events;
using OrderService.Domain.Interfaces;
using System.Threading.Tasks;

namespace OrderService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        //private readonly EventBusRabbitMQ _eventBus;
        private readonly IEventBus _eventBusShared;
        public OrderController(IOrderRepository orderRepository, IEventBus eventBusShared)
        {
            _orderRepository = orderRepository;
           // _eventBus = eventBus;
            _eventBusShared = eventBusShared;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            await _orderRepository.AddOrderAsync(order);

            // Publish an event to RabbitMQ
            var orderCreatedEvent = new OrderCreatedEvent
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                CustomerName = order.CustomerName
            };
           // _eventBus.PublishOrderCreatedEvent(orderCreatedEvent);
            _eventBusShared.Publish("OrderCreated", orderCreatedEvent);
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            await _orderRepository.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
