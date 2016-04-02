using Common.AOP.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.ConsolePlat
{
    [AOPProxyAttribute()]
    public class AOPCls : ContextBoundObject
    {
        public void MainMethod()
        {
            System.Console.WriteLine("{0}.{1}调用", "AOPCls", "MainMethod");
        }
    }
}
