using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost.Channels;

namespace WebApi.Display
{
    public class HttpBingDemo
    {
        public static void MainBySelfServer()
        {
            using (MyHttpSelfHostServer httpServer = new MyHttpSelfHostServer(new System.Web.Http.HttpConfiguration(), new Uri("http://127.0.0.1:3721")))
            {
                httpServer.Configuration.Routes.MapHttpRoute(
                        name: "DefaultApi",
                        routeTemplate: "api/{controller}/{id}",
                        defaults: new { id = RouteParameter.Optional });

                httpServer.Open();
            }
        }

        public static void MainMethod()
        {
            Uri listerUri = new Uri("http://127.0.0.1:3721");
            Binding binding = new HttpBinding();


            //创建、开启监听信道
            IChannelListener<IReplyChannel> channelListener = binding.BuildChannelListener<IReplyChannel>(listerUri);
            channelListener.Open();

            //创建开启回复信道
            IReplyChannel channel = channelListener.AcceptChannel();
            channel.Open();

            //开始监听
            while (true)
            {
                //接收输出请求消息
                RequestContext requestContext = channel.ReceiveRequest(TimeSpan.MaxValue);
                PrintRequestMessage(requestContext.RequestMessage);
                //消息回复
                requestContext.Reply(CreateResponseMessage());
            }
        }

        private static void PrintRequestMessage(Message message)
        {
            MethodInfo method = message.GetType().GetMethod("GetHttpRequestMessage");
            HttpRequestMessage request = (HttpRequestMessage)method.Invoke(message, new object[] { false });

            Console.WriteLine("{0, -15}:{1}", "RequestUri", request.RequestUri);

            foreach (var header in request.Headers)
            {
                Console.WriteLine("{0, -15}:{1}", header.Key, string.Join(",", header.Value.ToArray()));
            }
        }

        private static Message CreateResponseMessage()
        {
            HttpResponseMessage response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            Employee employee = new Employee("001", "Zhang San", "123456", "zhangsan@gmail.com");
            response.Content = new ObjectContent<Employee>(employee, new JsonMediaTypeFormatter());
            string httpMessageTypeName = "System.Web.Http.SelfHost.Channels.HttpMessage, System.Web.Http.SelfHost";
            Type httpMessageType = Type.GetType(httpMessageTypeName);
            return (Message)Activator.CreateInstance(httpMessageType, new object[] { response });
        }
    }

    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAddress { get; set; }

        public Employee(string id, string name, string phoneNo, string emailAddress)
        {
            this.Id = id;
            this.Name = name;
            this.PhoneNo = phoneNo;
            this.EmailAddress = emailAddress;
        }
    }
}
