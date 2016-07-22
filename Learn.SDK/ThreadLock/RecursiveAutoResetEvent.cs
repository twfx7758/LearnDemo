using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK.ThreadLock
{
    /// <summary>
    /// 替代Mutex（互斥体），用AutoResetEvent封装一个递归锁
    /// </summary>
    public sealed class RecursiveAutoResetEvent : IDisposable
    {
        private AutoResetEvent m_lock = new AutoResetEvent(true);
        private Int32 m_owningThreadId = 0;
        private Int32 m_recursionCount = 0;

        public void Enter()
        {
            Int32 currentThreadId = Thread.CurrentThread.ManagedThreadId;
            if (currentThreadId == m_owningThreadId)
            {
                m_recursionCount++;
                return;
            }
            //调用线程不拥有锁，等待它
            m_lock.WaitOne();
            //调用线程现在拥有了锁，初始化拥有线程的ID和递归计数
            m_owningThreadId = currentThreadId;
            m_recursionCount = 1;
        }

        public void Leave()
        {
            Int32 currentThreadId = Thread.CurrentThread.ManagedThreadId;
            //如果调用线程不拥有锁，就抛出错误
            if (currentThreadId != m_owningThreadId)
                throw new InvalidOperationException();
            //从递归计数中减一
            if (--m_recursionCount == 0)
            {
                m_owningThreadId = 0;
                m_lock.Set();
            }
        }
        public void Dispose()
        {
            m_lock.Dispose();
            m_owningThreadId = 0;
            m_recursionCount = 0;
        }
    }
}
