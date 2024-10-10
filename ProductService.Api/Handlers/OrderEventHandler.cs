using EventBus.Events;
using EventBus.Interfaces;

namespace ProductService.Api.Handlers
{
    public class OrderEventHandler
    {
        private readonly IEventBus _eventBus;

        public OrderEventHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe("OrderCreated", HandleOrderCreatedEvent);
        }

        private void HandleOrderCreatedEvent(object eventData)
        {
            var orderCreatedEvent = eventData as OrderCreatedEvent;
            if (orderCreatedEvent != null)
            {
                Console.WriteLine($"Received OrderCreatedEvent: OrderId={orderCreatedEvent.OrderId}, CustomerName={orderCreatedEvent.CustomerName}");
                // Handle the event (e.g., check the product stock)
            }
        }
    }
}
