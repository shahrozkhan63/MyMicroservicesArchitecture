using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using EventBus.Interfaces;

namespace EventBus.RabbitMQ
{
    public class RabbitMQEventBus : IEventBus
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQEventBus(string rabbitMqUri)
        {
            var factory = new ConnectionFactory() { Uri = new Uri(rabbitMqUri) };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Publish(string eventName, object data)
        {
            _channel.ExchangeDeclare(exchange: "EventBusExchange", type: ExchangeType.Fanout);

            var message = JsonSerializer.Serialize(data);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "EventBusExchange",
                                  routingKey: eventName,
                                  basicProperties: null,
                                  body: body);
        }

        public void Subscribe(string eventName, Action<object> eventHandler)
        {
            _channel.ExchangeDeclare(exchange: "EventBusExchange", type: ExchangeType.Fanout);
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: "EventBusExchange", routingKey: eventName);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var eventData = JsonSerializer.Deserialize<object>(message);
                eventHandler(eventData);
            };

            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}
