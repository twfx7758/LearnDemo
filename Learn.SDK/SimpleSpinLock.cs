using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK
{
    public class SimpleSpinLock{
        private Int32 m_ResourceInUse;

        public void Enter() {
            //将资源设置为“正在使用”（1），如果这个线程是把它从“自由使用”（0）
            //变成“正在使用”（1），就返回，以便执行Enter()调用之后的代码
            while (Interlocked.Exchange(ref m_ResourceInUse, 1) != 0) { 
                /*Black Magic*/
            }
        }

        public void Leave() { 
            //将资源标志为“自由使用”(0)
            Thread.VolatileWrite(ref m_ResourceInUse, 0);
        }
    }
}
