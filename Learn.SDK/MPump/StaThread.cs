using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK.MPump
{
    internal class StaThread
    {
        private Thread mStaThread;
        private IQueueReader<SendOrPostCallBackItem> mQueueConsumer;
        private ManualResetEvent mStopEvent = new ManualResetEvent(false);

        internal StaThread(IQueueReader<SendOrPostCallBackItem> reader)
        {
            mQueueConsumer = reader;
            mStaThread = new Thread(Run);
            mStaThread.Name = "STA Worker Thread";
            mStaThread.SetApartmentState(ApartmentState.STA);
        }

        internal void Start()
        {
            mStaThread.Start();
        }

        internal void Join()
        {
            mStaThread.Join();
        }

        private void Run()
        {
            while (true)
            {
                bool stop = mStopEvent.WaitOne();
                if (stop)
                {
                    break;
                }

                SendOrPostCallBackItem workItem = mQueueConsumer.Dequeue();
                if (workItem != null)
                    workItem.Execute();
            }
        }

        internal void Stop()
        {
            mStopEvent.Set();
            mQueueConsumer.ReleaseReader();
            mStaThread.Join();
            mQueueConsumer.Dispose();
        }
    }
}
