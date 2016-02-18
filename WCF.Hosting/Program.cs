using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using WCF.Service.Interface;
using WCF.Service;
using System.ServiceModel.Description;

namespace WCF.Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            HostByBaseAddress();
        }

        /// <summary>
        /// 基地址+相对地址
        /// </summary>
        static void HostByBaseAddress()
        {
            Uri[] baseAddress = new Uri[2];
            baseAddress[0] = new Uri("http://127.0.0.1/myservices");
            baseAddress[1] = new Uri("net.tcp://127.0.0.1/myservices");
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService), baseAddress))
            {
                //在配置里添加Behaviors
                host.AddServiceEndpoint(typeof(ICalculate), new WSHttpBinding(), "calculatorservice");
                host.AddServiceEndpoint(typeof(ICalculate), new NetTcpBinding(), "calculatorservice");
                //if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                //{
                //    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                //    behavior.HttpGetEnabled = true;
                //    behavior.HttpGetUrl = new Uri("http://127.0.0.1/calculatorservice");
                //    host.Description.Behaviors.Add(behavior);
                //}

                host.Opened += (s, e) => { Console.Write("CalculatorService服务已经启动，按任意键终止服务！"); };

                host.Open();

                Console.ReadLine();
            }
        }
    }
}
