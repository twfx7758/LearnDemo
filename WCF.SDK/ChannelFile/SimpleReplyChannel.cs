using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCF.SDK.ChannelFile
{
    public class SimpleReplyChannel : SimpleChannelBase, IReplyChannel
    {
        public IReplyChannel InnerReplyChannel
        {
            get { return (IReplyChannel)this.InnerChannel; }
        }

        public SimpleReplyChannel(ChannelManagerBase channelManager, IReplyChannel innerChannel)
            : base(channelManager, (ChannelBase)innerChannel)
        {
            this.Print("SimpleReplyChannel()");
        }

        public EndpointAddress LocalAddress
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
        {
            //throw new NotImplementedException();
            this.Print("BeginReceiveRequest()");
            return this.InnerReplyChannel.BeginReceiveRequest(timeout, callback, state);
        }

        public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public RequestContext EndReceiveRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context)
        {
            throw new NotImplementedException();
        }

        public bool EndWaitForRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public RequestContext ReceiveRequest()
        {
            throw new NotImplementedException();
        }

        public RequestContext ReceiveRequest(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public bool TryReceiveRequest(TimeSpan timeout, out RequestContext context)
        {
            throw new NotImplementedException();
        }

        public bool WaitForRequest(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }
    }
}
