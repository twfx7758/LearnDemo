using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Learn.SDK
{
    sealed internal class SampleHybridLock : IDisposable
    {
        private Int32 m_waiters = 0;
        private AutoResetEvent m_waiterlock = new AutoResetEvent(false);

        public void Enter()
        {
            if (Interlocked.Increment(ref m_waiters) == 1)
                return;

            m_waiterlock.WaitOne();
        }

        public void Leave()
        {
            if (Interlocked.Decrement(ref m_waiters) == 0)
                return;

            m_waiterlock.Set();
        }

        public void Dispose()
        {
            m_waiterlock.Dispose();
        }
    }
}
