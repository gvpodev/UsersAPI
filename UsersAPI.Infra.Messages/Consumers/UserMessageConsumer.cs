using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Messages.Models;
using UsersAPI.Infra.Messages.Services;
using UsersAPI.Infra.Messages.Settings;

namespace UsersAPI.Infra.Messages.Consumers
{
    public class UserMessageConsumer : BackgroundService
    {
        private readonly EmailMessageInfraService? _messageInfraService;
        private readonly IServiceProvider? _serviceProvider;
        private readonly RabbitMQSettings? _rabbitmqSettings;

        private IConnection? _connection;
        private IModel? _model;

        public UserMessageConsumer(EmailMessageInfraService? messageInfraService,
            IServiceProvider? serviceProvider,
            IOptions<RabbitMQSettings> rabbitmqSettings)
        {
            _messageInfraService = messageInfraService;
            _serviceProvider = serviceProvider;
            _rabbitmqSettings = rabbitmqSettings.Value;

            var factory = new ConnectionFactory { Uri = new Uri(_rabbitmqSettings?.Url) };
            _connection = factory.CreateConnection();
            _model = _connection.CreateModel();

            _model.QueueDeclare(
                queue: _rabbitmqSettings.Queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_model);

            consumer.Received += async (sender, args) =>
            {
                var payload = Encoding.UTF8.GetString(args.Body.ToArray());
                var userMessageVO = JsonConvert.DeserializeObject<UserMessageVO>(payload);

                using (var scope = _serviceProvider.CreateScope())
                {
                    var messageRequestModel = new MessageRequestModel
                    {
                        MailTo = userMessageVO.To,
                        Subject = userMessageVO.Subject,
                        Body = userMessageVO.Body,
                        IsBodyHtml = true
                    };

                    try
                    {
                        await _messageInfraService?.SendMessageAsync(messageRequestModel);
                        _model?.BasicAck(args.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        _model?.BasicNack(args.DeliveryTag, false, true);
                    }
                }
            };

            _model?.BasicConsume(_rabbitmqSettings?.Queue, false, consumer);
        }
    }
}