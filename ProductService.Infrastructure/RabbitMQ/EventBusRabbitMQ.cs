using ProductService.Domain.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductService.Infrastructure.RabbitMQ
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

        public void PublishProductCreatedEvent(ProductCreatedEvent productCreatedEvent)
        {
            _channel.ExchangeDeclare(exchange: "ProductExchange", type: ExchangeType.Fanout);
            var message = JsonSerializer.Serialize(productCreatedEvent);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "ProductExchange",
                                  routingKey: "",
                                  basicProperties: null,
                                  body: body);
        }
    }
}
