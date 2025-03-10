using RabbitMQ.Client;
using System.Text;

namespace AgendaApi.Application.RabbitMQ
{
    public interface IMessagePublisher
    {
        void Publish(string message);
    }

    public class MessagePublisher : IMessagePublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessagePublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "contatos",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(
                exchange: "contacts_exchange",
                routingKey: "",
                basicProperties: null,
                body: body);
        }
    }
}
