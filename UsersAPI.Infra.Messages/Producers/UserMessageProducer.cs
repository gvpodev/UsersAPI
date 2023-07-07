using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using UsersAPI.Domain.Interfaces.Messages;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Messages.Settings;

namespace UsersAPI.Infra.Messages.Producers
{
    public class UserMessageProducer : IUserMessageProducer
    {
        private readonly RabbitMQSettings? _rabbitMqSettings;

        public UserMessageProducer(IOptions<RabbitMQSettings?> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
        }

        public void Send(UserMessageVO userMessage)
        {
            if (_rabbitMqSettings?.Url != null)
            {
                var connectionFactory = new ConnectionFactory
                {
                    Uri = new Uri(_rabbitMqSettings?.Url)
                };
                
                // conecta no rabbit mq
                using (var connection = connectionFactory.CreateConnection())
                {
                    // cria uma fila
                    using (var model = connection.CreateModel())
                    {
                        model.QueueDeclare(
                            queue: _rabbitMqSettings.Queue,
                            durable: true,
                            autoDelete: false,
                            exclusive: false,
                            arguments: null
                        );
                        
                        model.BasicPublish(
                            exchange: string.Empty,
                            routingKey: _rabbitMqSettings.Queue,
                            basicProperties: null,
                            body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userMessage))
                        );
                    }
                }
            }
        }
    }
}
