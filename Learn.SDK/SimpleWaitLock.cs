using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK
{
    public class SimpleWaitLock : IDisposable
    {
        private AutoResetEvent m_ResourceFree = new AutoResetEvent(true);//最初可以自由使用

        public void Enter() {
            //在内核中阻塞，等待资源，然后返回
            m_ResourceFree.WaitOne();
        }


        public void Leave() {
            m_ResourceFree.Set();//将资源标记为“自由使用”(Free)
        }

        public void Dispose() {
            m_ResourceFree.Dispose();
        }
    }
}
