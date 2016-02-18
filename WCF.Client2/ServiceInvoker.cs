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
            //不需要在web.config里配置终结点
            using (ChannelFactory<ICalculate> channelFactory =
                new ChannelFactory<ICalculate>(new WSHttpBinding(), "http://127.0.0.1:8888/calculatorservice"))
            {
                ICalculate client = channelFactory.CreateChannel();

                Console.WriteLine("计算结果：{0}", client.Add(3, 7));
            }
        }

        public void Add(int x, int b)
        {
            //在web.config里配置终结点
            using (ChannelFactory<ICalculate> channelFactory = new ChannelFactory<ICalculate>("calculatorservice"))
            {
                ICalculate client = channelFactory.CreateChannel();

                Console.WriteLine("计算结果：{0}", client.Add(3, 7));
            }
        }
        
    }
}
