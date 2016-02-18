using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WCF.Client
{
    internal class ServiceInvoker
    {
        public async void Add(double x, double y)
        {
            Api.Wcf.Calculator.CalculateClient client = new Api.Wcf.Calculator.CalculateClient();
            Console.WriteLine("1线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            Task<double> addVal = client.AddAsync(4, 6);
            Console.WriteLine("异步调用了Calculator服务的Add操作");
            double lastVal = await addVal;
            Console.WriteLine("2线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("计算结果：{0}", lastVal);
        }
    }
}
