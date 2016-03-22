using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WCF.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("main线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            //new ServiceInvoker().Add(10, 12.5);

            //监听服务
            //ServiceListener();

            //测试延迟执行关键字yield
            //new ServiceInvoker().MainMethod();

            //获取服务调用端的IP地址
            new ServiceInvoker().GetWasHost();

            Console.ReadLine();
        }


        static void ServiceListener()
        {
            Uri listUri = new Uri("http://127.0.0.1:3721/listener");
            Binding binding = new BasicHttpBinding();
            //创建、开启信道监听器
            IChannelListener<IReplyChannel> channelListener = binding.BuildChannelListener<IReplyChannel>(listUri);
            channelListener.Open();

            //创建、开启回复信道
            IReplyChannel channel = channelListener.AcceptChannel(TimeSpan.MaxValue);
            channel.Open();

            //开始监听
            while (true)
            {
                //接收输出请求消息
                RequestContext requestContext = channel.ReceiveRequest(TimeSpan.MaxValue);
                Console.WriteLine(requestContext.RequestMessage);

                //消息回复
                requestContext.Reply(CreateReplyMessage(binding));
            }
        }

        static Message CreateReplyMessage(Binding binding)
        {
            string action = "http://www.kf.com/CalculatorService/AddResponse";
            XNamespace ns = "http://www.kf.com";
            XElement body = new XElement(new XElement(ns + "AddResponse", new XElement(ns + "AddResult", 3)));
            return Message.CreateMessage(binding.MessageVersion, action, body);
        }
    }
}
