﻿using RabbitMQ.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    [Serializable]
    public class EventMessage<T> : IEventMessage<T>
    {
        public byte deliveryMode{ get; set;}
        public T MessageEntity { get; set; }
        public bool IsOperationOk { get; set; }
        public IEventMessage<T> BuildEventMessageResult(byte[] body)
        {
            return MessageSerializerFactory.CreateMessageSerializerInstance().BytesDeseriallizer<IEventMessage<T>>(body);
        }
    }
}
