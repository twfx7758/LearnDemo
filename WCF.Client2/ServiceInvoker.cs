using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Service.Interface;
using System.ServiceModel;

namespace WCF.Client2
{
    public class ServiceInvoker
    {
        public void Add(double x, double y)
        {
            using (ChannelFactory<ICalculate> ChannelFactory =
                new ChannelFactory<ICalculate>(new WSHttpBinding(), "http://127.0.0.1:8888/calculatorservice"))
            {
                ICalculate client = ChannelFactory.CreateChannel();

                Console.WriteLine("计算结果：{0}", client.Add(3, 7));
            }
        }
        
    }
}
