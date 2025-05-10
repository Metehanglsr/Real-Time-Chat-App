using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChatApp.Application.Abstractions.Services.RabbitMq
{
    public interface IRabbitMQProducer
    {
        Task Publish(string name);
    }
}
