using Newbie.AOP;
using Newbie.AOP.BasicHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.AOP.Test
{
    public class AopInvocation
    {
        public static void MainMethod()
        {
            InstanceBuilder.Create<AopCls, IAopCls>().GetProcessResult(3);
        }
    }

    public class AopCls : IAopCls
    {
        [ExceptionCallHandler(Ordinal = 1, MessageTemplate = "Encounter error:\nMessage:{Message}")]
        public void GetProcessResult(int args)
        {
            if (args < 10)
            {
                throw new Exception("抛出异常信息");
            }
        }
    }

    public interface IAopCls
    {
        void GetProcessResult(int args);
    }
}
