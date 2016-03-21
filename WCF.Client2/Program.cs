using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WCF.Service.Interface;

namespace WCF.Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            new ServiceInvoker().Add(10.1, 12.2);
            //客户端通道发送消息
            //SendMessageClient();
            //DemoServiceClient();

            //WBinding.BindMain();
            //WMessage.MessageHeaderMain();

            Console.ReadLine();
        }

        #region 客户端通道发送消息
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
        #endregion

        #region 服务程序的调用
        static void DemoServiceClient()
        {
            using (ChannelFactory<IDemoService> factory = new ChannelFactory<IDemoService>("DemoService"))
            {
                IDemoService demoService = factory.CreateChannel();
                var items = demoService.GetItems("");
                items.ToArray();
            }

            Console.ReadLine();
        }
        #endregion
        
    }
}
