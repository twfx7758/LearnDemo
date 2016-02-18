using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WCF.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("main线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            new ServiceInvoker().Add(10, 12.5);
            Console.ReadLine();
        }
    }
}
