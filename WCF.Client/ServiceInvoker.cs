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
        public void MainMethod()
        {
            IEnumerable<string> items = GetItems();
            Console.WriteLine("begin to iterate the collection.");
            items.ToArray();
        }

        IEnumerable<string> GetItems()
        {
            Console.WriteLine("Begin to invoke GetItems() method.");
            yield return "Foo";
            yield return "Bar";
            yield return "Baz";
        }

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


        public async void GetWasHost()
        {
            Api.Wcf.WAS.WasHostClient client = new Api.Wcf.WAS.WasHostClient();
            var result = await client.HelloWCFAsync();
            Console.WriteLine(result);
            client.Close();
        }
    }
}
