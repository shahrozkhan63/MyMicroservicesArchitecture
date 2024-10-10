using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace OrderService.Api.Services
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IConfiguration _config;

        public RabbitMQConsumer(IConfiguration config)
        {
            _config = config;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory() { HostName = _config["RabbitMQ:Host"] };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "order.exchange", type: ExchangeType.Direct);
            channel.QueueDeclare(queue: "order.queue", exclusive: false);
            channel.QueueBind(queue: "order.queue", exchange: "order.exchange", routingKey: "order");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received Order: {message}");
            };

            channel.BasicConsume(queue: "order.queue", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
