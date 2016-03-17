using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    public class MessageSerializer
    {
        public byte[] SerializerBytes(EventMessage message)
        {
            return new byte[2];
        }
    }
}
