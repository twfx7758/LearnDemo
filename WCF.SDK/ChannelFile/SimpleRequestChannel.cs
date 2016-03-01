using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCF.SDK.ChannelFile
{
    public class SimpleRequestChannel : SimpleChannelBase, IRequestChannel
    {
        public IRequestChannel InnerRequestChannel
        {
            get { return (IRequestChannel)this.InnerChannel; }
        }

        public EndpointAddress RemoteAddress
        {
            get
            {
                return InnerRequestChannel.RemoteAddress;
            }
        }

        public Uri Via
        {
            get
            {
                return InnerRequestChannel.Via;
            }
        }

        public SimpleRequestChannel(ChannelManagerBase channelManager, IRequestChannel innerChannel)
            : base(channelManager, (ChannelBase)innerChannel)
        { }

        public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            //throw new NotImplementedException();
            this.Print("BeginRequest()");
            return this.InnerRequestChannel.BeginRequest(message, timeout, callback, state);
        }

        public Message EndRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public Message Request(Message message)
        {
            throw new NotImplementedException();
        }

        public Message Request(Message message, TimeSpan timeout)
        {
            throw new NotImplementedException();
        }
    }
}
