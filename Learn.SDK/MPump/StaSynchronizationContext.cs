using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK.MPump
{
    public class StaSynchronizationContext : SynchronizationContext, IDisposable
    {
        private BlockingQueue<SendOrPostCallBackItem> mQueue;
        private StaThread mStaThread;

        public StaSynchronizationContext()
            : base()
        {
            mQueue = new BlockingQueue<SendOrPostCallBackItem>();
            mStaThread = new StaThread(mQueue);
            mStaThread.Start();
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            SendOrPostCallBackItem item = new MPump.SendOrPostCallBackItem(d, state, ExecutionType.Send);
            mQueue.Enqueue(item);
            item.ExecutionCompleteWaitHandle.WaitOne();
            if (item.ExecutedWithException)
                throw item.Exception;
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            SendOrPostCallBackItem item = new MPump.SendOrPostCallBackItem(d, state, ExecutionType.Post);
            mQueue.Enqueue(item);
        }

        public void Dispose()
        {
            mStaThread.Stop();
        }

        public override SynchronizationContext CreateCopy()
        {
            return this;
        }
    }
}
