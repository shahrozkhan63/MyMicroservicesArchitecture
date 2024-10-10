using RabbitMQ.Client;
using System.Text;

namespace ProductService.Api.Services
{
    public class RabbitMQPublisher
    {
        private readonly IConfiguration _config;

        public RabbitMQPublisher(IConfiguration config)
        {
            _config = config;
        }

        public void PublishProduct(string message)
        {
            var factory = new ConnectionFactory() { HostName = _config["RabbitMQ:Host"] };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "product.exchange", type: ExchangeType.Direct);
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "product.exchange", routingKey: "product", body: body);
        }
    }
}
