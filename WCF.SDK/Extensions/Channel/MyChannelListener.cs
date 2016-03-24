using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace WCF.SDK.Extensions.Channel
{
    public class MyChannelListener<TChannel> : ChannelListenerBase<TChannel> where TChannel : class, IChannel
    {
        private IChannelListener<TChannel> InnerChannelListener
        { get; set; }

        public MyChannelListener(BindingContext context)
        {
            this.InnerChannelListener = context.BuildInnerChannelListener<TChannel>();
        }

        protected override TChannel OnAcceptChannel(TimeSpan timeout)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnAcceptChannel()");
            TChannel innerChannel = this.InnerChannelListener.AcceptChannel(timeout);
            return new MyReplyChannel(this, innerChannel as IReplyChannel) as TChannel;
        }

        protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnBeginAcceptChannel()");
           return this.InnerChannelListener.BeginAcceptChannel(timeout, callback, state);
        }

        protected override TChannel OnEndAcceptChannel(IAsyncResult result)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnEndAcceptChannel()");
            TChannel innerChannel = this.InnerChannelListener.EndAcceptChannel(result);
            return new MyReplyChannel(this, innerChannel as IReplyChannel) as TChannel;
        }

        protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnBeginWaitForChannel()");
            return this.InnerChannelListener.BeginWaitForChannel(timeout, callback, state);
        }

        protected override bool OnEndWaitForChannel(IAsyncResult result)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnEndWaitForChannel()");
            return this.InnerChannelListener.EndWaitForChannel(result);
        }

        protected override bool OnWaitForChannel(TimeSpan timeout)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnWaitForChannel()");
            return this.InnerChannelListener.WaitForChannel(timeout);
        }

        public override Uri Uri
        {
            get 
            {
                Console.WriteLine("MyChannelListener<TChannel>.Uri"); 
                return this.InnerChannelListener.Uri;
            }

        }

        protected override void OnAbort()
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnAbort()");
            this.InnerChannelListener.Abort();
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnBeginClose()");
            return this.InnerChannelListener.BeginClose(timeout, callback, state);
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnBeginOpen()");
            return this.InnerChannelListener.BeginOpen(timeout, callback, state);
        }

        protected override void OnClose(TimeSpan timeout)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnClose()");
            this.InnerChannelListener.Close(timeout);
        }

        protected override void OnEndClose(IAsyncResult result)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnEndClose()");
            this.InnerChannelListener.EndClose(result);
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnEndOpen()");
            this.InnerChannelListener.EndOpen(result);
        }

        protected override void OnOpen(TimeSpan timeout)
        {
            Console.WriteLine("MyChannelListener<TChannel>.OnOpen()");
            this.InnerChannelListener.Open(timeout);
        }
    }
}
