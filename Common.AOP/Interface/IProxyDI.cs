using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AOP.Interface
{
    public interface IProxyDI
    {
        void InterceptionDI(IInterception interception);
    }
}
