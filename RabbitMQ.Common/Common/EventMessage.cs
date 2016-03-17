using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    [Serializable]
    public class EventMessage : IEventMessage
    {
        public bool IsOperationOk { get; set; }

        public string MessageContent { get; set; } 

        public IEventMessage BuildEventMessageResult(byte[] body)
        {
            return MessageSerializerFactory.CreateMessageSerializerInstance().BytesDeseriallizer<IEventMessage>(body);
        }
    }
}
