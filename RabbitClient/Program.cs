using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Common;

namespace RabbitClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RabbitMQTest();

            Console.ReadKey();
        }

        //测试RabbitMQ
        static void RabbitMQTest()
        {
            RabbitMQClientContext context = new RabbitMQClientContext();

            EventMessage message = new EventMessage() {
                IsOperationOk = false,
                MessageContent = "测试客户端类库"
            };

            RabbitMQSender sender = new RabbitMQSender() { Context = context };
            sender.TriggerEventMessage(message, "", "Info");
        }
    }
}
