using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCF.SDK.ChannelStack
{
    public abstract class SimpleChannelListenerBase<TChannel> 
        : ChannelListenerBase<TChannel> where TChannel : class, IChannel
    {
        protected void Print(string methodName)
        {
            Console.WriteLine("{0}.{1}", this.GetType().Name, methodName);
        }

        public IChannelListener<TChannel> InnerChannelListener { get; private set; }

        public SimpleChannelListenerBase(BindingContext context)
        {
            this.InnerChannelListener = context.BuildInnerChannelListener<TChannel>();
        }

        protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
        {
            this.Print("OnBeginAcceptChannel()");
            return this.InnerChannelListener.BeginAcceptChannel(timeout, callback, state);
        }

        public override T GetProperty<T>()
        {
            return this.InnerChannelListener.GetProperty<T>();
        }
    }
}
