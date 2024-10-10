
using RabbitMQ.Client;
using System.Text;

public class RabbitMQPublisher
{
    private readonly IConfiguration _config;

    public RabbitMQPublisher(IConfiguration config)
    {
        _config = config;
    }

    public void PublishOrder(string message)
    {
        var factory = new ConnectionFactory() { HostName = _config["RabbitMQ:Host"] };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "order.exchange", type: ExchangeType.Direct);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "order.exchange", routingKey: "order", body: body);
    }
}
