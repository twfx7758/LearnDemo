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
            RabbitMQClientContext context = new RabbitMQClientContext();
            RabbitMQConsumer consumer = new RabbitMQConsumer() {
                 Context = context,
                 ActionMessage = b => {
                     Console.WriteLine(b.MessageContent);
                     b.IsOperationOk = true;
                 }
            };

            consumer.OnListening();
        }
    }
}
