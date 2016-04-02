using Common.AOP.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AOP
{
    public class ProxyFactory
    {
        public static T CreateProxyInstance<T>(IInterception interception) where T : new()
        {
            Type serverType = typeof(T);
            MarshalByRefObject target = Activator.CreateInstance(serverType) as MarshalByRefObject;
            AOPRealProxy aopRealProxy = new AOPRealProxy(serverType, target);
            aopRealProxy.InterceptionDI(interception);
            return (T)aopRealProxy.GetTransparentProxy();
        }
    }
}
