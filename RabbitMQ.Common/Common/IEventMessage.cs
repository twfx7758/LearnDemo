using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    public interface IEventMessage
    {
        bool IsOperationOk { get; set; }
        string MessageContent { get; set; }
        IEventMessage BuildEventMessageResult(byte[] body);
    }
}
