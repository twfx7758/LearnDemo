using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using WCF.Service.Interface;

namespace WCF.Service
{
    public class WASHostService : IWASHostService
    {
        public string HelloWCF(string str)
        {
            var context = OperationContext.Current;
            var properties = context.IncomingMessageProperties;
            var endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            Console.WriteLine("客户端地址：{0},端口号：{1}", endpoint.Address, endpoint.Port.ToString());

            return string.Format("Get message from client is : {0}", str);
        }
    }
}
