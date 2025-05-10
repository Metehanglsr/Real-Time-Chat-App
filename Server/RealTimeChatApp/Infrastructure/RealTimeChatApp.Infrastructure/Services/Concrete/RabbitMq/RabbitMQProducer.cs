using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RealTimeChatApp.Application.Abstractions.Services.RabbitMq;

namespace RealTimeChatApp.Infrastructure.Services.Concrete.RabbitMq
{
    public sealed class RabbitMQProducer : IRabbitMQProducer
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMQProducer()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;

            _channel.QueueDeclareAsync(
                queue: "userLog",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null).Wait();
        }

        public async Task Publish(string name)
        {
            var body = Encoding.UTF8.GetBytes(name);
            var props = new BasicProperties { Persistent = true };

            await _channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: "userLog",
                mandatory: true,
                basicProperties: props,
                body: body);
        }
    }
}