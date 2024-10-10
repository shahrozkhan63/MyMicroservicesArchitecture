using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using OrderService.Domain.Events;

namespace OrderService.Infrastructure.RabbitMQ
{
    public class EventBusRabbitMQ
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public EventBusRabbitMQ(string rabbitMqUri)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(rabbitMqUri) };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PublishOrderCreatedEvent(OrderCreatedEvent orderCreatedEvent)
        {
            _channel.ExchangeDeclare(exchange: "OrderExchange", type: ExchangeType.Fanout);
            var message = JsonSerializer.Serialize(orderCreatedEvent);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "OrderExchange",
                                  routingKey: "",
                                  basicProperties: null,
                                  body: body);
        }
    }
}
