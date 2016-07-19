using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK
{
    public class NSynchronizationContext
    {
        public Task TestMethod()
        {
            SynchronizationContext context = SynchronizationContext.Current;
            int id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"TestMethod的线程ID：{id}");
            return Task.Run(()=>context.Post(MainMethod, null));
        }

        private void MainMethod(object state)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"TestMethod的线程ID：{id}");
        }
    }
}
