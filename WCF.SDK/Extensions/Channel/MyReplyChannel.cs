using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;

namespace WCF.SDK.Extensions.Channel
{
    public class MyReplyChannel: ChannelBase, IReplyChannel
    {
        private IReplyChannel InnerChannel
        { get; set; }

        public MyReplyChannel(ChannelManagerBase channelManager, IReplyChannel innerChannel):base(channelManager)
        {
            this.InnerChannel = innerChannel;
        }

        #region ChannelBase Members
        protected override void OnAbort()
        {
            Console.WriteLine("MyReplyChannel.OnAbort()");
            this.InnerChannel.Abort();
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyReplyChannel.OnBeginClose()");
            return this.InnerChannel.BeginClose(timeout, callback, state);
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyReplyChannel.OnBeginOpen()");
            return this.InnerChannel.BeginOpen(timeout, callback, state);
        }

        protected override void OnClose(TimeSpan timeout)
        {
            Console.WriteLine("MyReplyChannel.OnClose()");
            this.Close(timeout);
        }

        protected override void OnEndClose(IAsyncResult result)
        {
            Console.WriteLine("MyReplyChannel.OnEndClose()");
            this.InnerChannel.EndClose(result);
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
            Console.WriteLine("MyReplyChannel.OnEndOpen()");
            this.InnerChannel.EndOpen(result);
        }

        protected override void OnOpen(TimeSpan timeout)
        {
            Console.WriteLine("MyReplyChannel.OnOpen()");
            this.InnerChannel.Open(timeout);
        }
        #endregion

        #region IReplyChannel Members

        public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyReplyChannel.BeginReceiveRequest()");
            return this.InnerChannel.BeginReceiveRequest(timeout, callback, state);
        }

        public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
        {
            Console.WriteLine("MyReplyChannel.BeginReceiveRequest()");
            return this.InnerChannel.BeginReceiveRequest(callback, state);
        }

        public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyReplyChannel.BeginTryReceiveRequest()");
            return this.InnerChannel.BeginTryReceiveRequest(timeout, callback, state);
        }

        public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyReplyChannel.BeginWaitForRequest()");
            return this.InnerChannel.BeginWaitForRequest(timeout, callback, state);
        }

        public RequestContext EndReceiveRequest(IAsyncResult result)
        {
            Console.WriteLine("MyReplyChannel.EndReceiveRequest()");
            return this.InnerChannel.EndReceiveRequest(result);
        }

        public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context)
        {
            Console.WriteLine("MyReplyChannel.EndTryReceiveRequest()");
            return this.InnerChannel.EndTryReceiveRequest(result, out context);
        }

        public bool EndWaitForRequest(IAsyncResult result)
        {
            Console.WriteLine("MyReplyChannel.EndWaitForRequest()");
            return this.InnerChannel.EndWaitForRequest(result);
        }

        public System.ServiceModel.EndpointAddress LocalAddress
        {
            get 
            {
                Console.WriteLine("MyReplyChannel.LocalAddress");
                return this.InnerChannel.LocalAddress;
            }
        }

        public RequestContext ReceiveRequest(TimeSpan timeout)
        {
            Console.WriteLine("MyReplyChannel.ReceiveRequest()");
            return this.InnerChannel.ReceiveRequest(timeout);
        }

        public RequestContext ReceiveRequest()
        {
            Console.WriteLine("MyReplyChannel.ReceiveRequest()");
            return this.InnerChannel.ReceiveRequest();
        }

        public bool TryReceiveRequest(TimeSpan timeout, out RequestContext context)
        {
            Console.WriteLine("MyReplyChannel.TryReceiveRequest()");
            return this.InnerChannel.TryReceiveRequest(timeout, out context);
        }

        public bool WaitForRequest(TimeSpan timeout)
        {
            Console.WriteLine("MyReplyChannel.WaitForRequest()");
            return this.InnerChannel.WaitForRequest(timeout);
        }

        #endregion
    }
}
