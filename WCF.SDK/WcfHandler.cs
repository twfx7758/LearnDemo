using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WCF.SDK
{
    public class WcfHandler : IHttpHandler
    {
        public Type ServiceType { get; private set; }
        public MessageEncoderFactory MessageEncoderFactory { get; private set; }
        public IDictionary<string, MethodInfo> Methods { get; private set; }
        public IDictionary<string, IDispatchMessageFormatter> MessageFormatters { get; private set; }
        public IDictionary<string, IOperationInvoker> OperationInvokers { get; private set; }

        public WcfHandler(Type serviceType, MessageEncoderFactory messageEncoderFactory)
        {
            this.ServiceType = serviceType;
            this.MessageEncoderFactory = messageEncoderFactory;
            this.Methods = new Dictionary<string, MethodInfo>();
            this.MessageFormatters = new Dictionary<string, IDispatchMessageFormatter>();
            this.OperationInvokers = new Dictionary<string, IOperationInvoker>();
        }

        #region IHttpHandler接口成员
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
