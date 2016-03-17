using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Common;

namespace RabbitService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================监听程序开启==================");

            RabbitMQTest();

            Console.ReadKey();
        }

        //测试RabbitMQ
        static void RabbitMQTest()
        {
            LogLocation.Log = new LogInfo();
            RabbitMQClientContext context = new RabbitMQClientContext() { ListenQueueName = "LogQueue" };
            RabbitMQConsumer<EventMessage<string>, string> consumer = new RabbitMQConsumer<EventMessage<string>, string>() {
                 Context = context,
                 ActionMessage = b => {
                     Console.WriteLine(b.MessageEntity);
                     b.IsOperationOk = true;
                 }
            };

            consumer.OnListening();
        }
    }
}
