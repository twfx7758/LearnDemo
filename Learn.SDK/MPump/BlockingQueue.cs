using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.SDK.MPump
{
    internal class BlockingQueue<T> : IQueueReader<T>, IQueueWriter<T>, IDisposable
    {
        //用一个.net队列存数据
        private Queue<T> mQueue = new Queue<T>();

        private Semaphore mSemaphore = new Semaphore(0, int.MaxValue);

        private ManualResetEvent mKillThread = new ManualResetEvent(false);

        private WaitHandle[] mWaitHandles;

        public BlockingQueue()
        {
            mWaitHandles = new WaitHandle[2] { mSemaphore, mKillThread };
        }

        public void Enqueue(T data)
        {
            lock (mQueue) mQueue.Enqueue(data);

            mSemaphore.Release();
        }

        public T Dequeue()
        {
            WaitHandle.WaitAny(mWaitHandles);
            lock (mQueue)
            {
                if (mQueue.Count > 0)
                    return mQueue.Dequeue();
            }

            return default(T);
        }

        public void ReleaseReader()
        {
            mKillThread.Set();
        }

        void IDisposable.Dispose()
        {
            if (mSemaphore != null)
            {
                mSemaphore.Close();
                mQueue.Clear();
                mSemaphore = null;
            }
        }
    }
}
