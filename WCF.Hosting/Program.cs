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
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService))) {
                //在配置里添加Behaviors
                //host.AddServiceEndpoint(typeof(ICalculate), new WSHttpBinding(), "http://127.0.0.1:8888/calculatorservice");
                //if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null) {
                //    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                //    behavior.HttpGetEnabled = true;
                //    behavior.HttpGetUrl = new Uri("http://127.0.0.1:8888/calculatorservice");
                //    host.Description.Behaviors.Add(behavior);
                //}

                host.Opened += (s, e) => { Console.Write("CalculatorService服务已经启动，按任意键终止服务！"); };

                host.Open();

                Console.ReadLine();
            }
        }
    }
}
