using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Learn.SDK.ThreadLock
{
    //ManualResetEventSlim
    //SemaphoreSlim
    //都包含了自己的自旋逻辑，
    //AutoResetEvent没有AutoResetEventSlim类，可以构造一个SemaphoreSlim对象，maxCount=1来代替
    //ReaderWriterLockSlim
    sealed internal class AnthorHybridLock : IDisposable
    {
        private Int32 m_waiters = 0;
        private AutoResetEvent m_waiterlock = new AutoResetEvent(false);
        //这个字段控制自旋，希望能提升性能
        private Int32 m_spincount = 4000;

        //这些字段指出哪些线程拥有锁，以及它拥有了它多少次
        private Int32 m_owningThreadId = 0, m_recursion = 0;
        public void Enter()
        {
            //如果调用线程已拥有锁，递增递归计数并返回
            Int32 threadId = Thread.CurrentThread.ManagedThreadId;
            if (threadId == m_owningThreadId) { m_recursion++; return; }

            //调用线程不拥有锁，尝试获取它
            SpinWait spinwait = new SpinWait();
            for (Int32 spincount = 0; spincount < m_spincount; spincount++)
            {
                //如果锁可以自由使用，这个线程就获得它：设置一些状态并返回
                if (Interlocked.CompareExchange(ref m_waiters, 1, 0) == 0) goto GotLock;

                //Black magic：给其它线程运行的机会，希望锁会释放。
                spinwait.SpinOnce();
            }

            //自旋（Spinning）结束，锁仍然没有获得，再试一次
            if (Interlocked.Increment(ref m_waiters) > 1)
            {
                //其它线程阻塞，这个线程也必须阻塞
                m_waiterlock.WaitOne();//等待锁：性能损失
                //等这个线程醒来时，它拥有锁：设置一些状态并返回。
            }

        GotLock:
            //一个线程拥有锁时，我们记录它的ID,并
            //指出线程拥有锁一次
            m_owningThreadId = threadId; m_recursion = 1;
        }

        public void Leave()
        {
            //如果调用线程不拥有锁，表明存在一个bug
            Int32 threadId = Thread.CurrentThread.ManagedThreadId;
            if (threadId != m_owningThreadId)
                throw new SynchronizationLockException("Lock not owned by calling thread");

            //递减递归计数，如果这个线程仍然拥有锁，那么直接返回
            if (--m_recursion > 0) return;

            m_recursion = 0;//现在没有线程拥有锁

            //如果没有其它线程被阻塞，直接返回
            if (Interlocked.Decrement(ref m_waiters) == 0)
            {
                return;
            }

            //有其它线程被阻塞，唤醒其中一个
            m_waiterlock.Set();//这个性能有较大损失
        }

        public void Dispose()
        {
            m_waiterlock.Dispose();
        }
    }
}
