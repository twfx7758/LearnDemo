using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace WCF.SDK.Extensions.Channel
{
    public class MyChannelFactory<TChannel> : ChannelFactoryBase<TChannel>
    {
        private IChannelFactory<TChannel> InnerChannelFactory
        { get; set; }

        public MyChannelFactory(BindingContext context)
        {
            this.InnerChannelFactory = context.BuildInnerChannelFactory<TChannel>();
        }

        protected override TChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            Console.WriteLine("MyChannelFactory<TChannel>.OnCreateChannel()");
            TChannel innerChannel = this.InnerChannelFactory.CreateChannel(address, via);
            return (TChannel)(object)(new MyRequestChannel(this, innerChannel as IRequestChannel));
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("MyChannelFactory<TChannel>.OnBeginOpen()");
            return this.InnerChannelFactory.BeginOpen(timeout, callback, state);
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
            Console.WriteLine("MyChannelFactory<TChannel>.OnEndOpen()");
            this.InnerChannelFactory.EndOpen(result);
        }

        protected override void OnOpen(TimeSpan timeout)
        {
            Console.WriteLine("MyChannelFactory<TChannel>.OnOpen()");
            this.InnerChannelFactory.Open();
        }
    }
}
