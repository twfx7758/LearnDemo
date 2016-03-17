using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Common
{
    public class MessageProcessResult
    {
        public EventMessage Message { get; set; }
        public bool IsOperationOk { get; set; }
    }
}
