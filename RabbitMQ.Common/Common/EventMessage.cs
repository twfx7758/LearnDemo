using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    [Serializable]
    public class EventMessage
    {
        public bool IsOperationOk { get; set; }

        public string MessageContent { get; set; } 

        public static EventMessage BuildEventMessageResult(byte[] body)
        {
            return MessageSerializerFactory.CreateMessageSerializerInstance().BytesDeseriallizer<EventMessage>(body);
        }
    }
}
