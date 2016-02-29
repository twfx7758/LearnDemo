using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WCF.Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            //new ServiceInvoker().Add(10, 12);

            //发送消息
            SendMessageClient();

            Console.ReadLine();
        }

        static void SendMessageClient()
        {
            Uri listUri = new Uri("http://127.0.0.1:3721/listener");
            Binding binding = new BasicHttpBinding();
            IChannelFactory<IRequestChannel> channelFactory = binding.BuildChannelFactory<IRequestChannel>();
            channelFactory.Open();

            //创建、开启请求信道
            IRequestChannel channel = channelFactory.CreateChannel(new EndpointAddress(listUri));
            channel.Open();

            var message = channel.Request(CreateRequestMessage(binding));
            Console.WriteLine(message);
        }

        static Message CreateRequestMessage(Binding binding)
        {
            string action = "http://www.kf.com/CalculatorService/AddResponse";
            XNamespace ns = "http://www.kf.com";
            XElement body = new XElement(new XElement(ns + "Add", 
                new XElement(ns + "x", 1),
                new XElement(ns + "y", 2)));
            return Message.CreateMessage(binding.MessageVersion, action, body);
        }
    }
}
