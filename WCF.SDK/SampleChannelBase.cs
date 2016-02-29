using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCF.SDK
{
    public abstract class SimpleChannelBase : ChannelBase
    {
        //其它成员
        public ChannelBase InnerChannel { get; private set; }//与本信息相连的下一个信道

        public SimpleChannelBase(ChannelManagerBase channelManager, ChannelBase innerChannel)
            : base(channelManager)
        {
            this.InnerChannel = innerChannel;
        }

        protected void Print(string methodName)
        {
            Console.WriteLine("{0}.{1}", this.GetType().Name, methodName);
        }

        //实现ChannelBase的抽象方法
        protected override void OnAbort()
        {
            //throw new NotImplementedException();
            this.Print("OnAbort()");
            this.InnerChannel.Abort();
        }

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            //throw new NotImplementedException();
            this.Print("OnBeginClose()");
            return this.InnerChannel.BeginClose(timeout, callback, state);
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            //throw new NotImplementedException();
            this.Print("OnBeginOpen()");
            return this.InnerChannel.BeginOpen(timeout, callback, state);
        }

        protected override void OnClose(TimeSpan timeout)
        {
            //throw new NotImplementedException();
            this.Print("OnClose()");
            this.InnerChannel.Close(timeout);
        }

        protected override void OnEndClose(IAsyncResult result)
        {
            this.Print("OnEndClose()");
            this.InnerChannel.EndClose(result);
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
            //throw new NotImplementedException();
            this.Print("OnEndOpen()");
            this.InnerChannel.EndOpen(result);
        }

        protected override void OnOpen(TimeSpan timeout)
        {
            //throw new NotImplementedException();
            this.Print("OnOpen()");
            this.InnerChannel.Open();
        }

        public override T GetProperty<T>()
        {
            return this.InnerChannel.GetProperty<T>();
        }
    }
}
