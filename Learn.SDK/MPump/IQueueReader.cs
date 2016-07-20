using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.SDK.MPump
{
    internal interface IQueueReader<T> : IDisposable
    {
        T Dequeue();
        void ReleaseReader();
    }
}
