using Common.AOP.Implementation;
using Common.AOP.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;

namespace Common.AOP.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AOPProxyAttribute : ProxyAttribute
    {
        private IInterception _interception;
        public Type Interception
        {
            get
            {
                return _interception.GetType();
            }
            set
            {
                IInterception interception = Activator.CreateInstance(value) as IInterception;
                _interception = interception;
            }
        }

        public AOPProxyAttribute()
        {
            _interception = new NullInterception();
        }

        public override MarshalByRefObject CreateInstance(Type serverType)
        {
            MarshalByRefObject target = base.CreateInstance(serverType);
            AOPRealProxy aopRealProxy = new AOPRealProxy(serverType, target);
            aopRealProxy.InterceptionDI(_interception);
            return aopRealProxy.GetTransparentProxy() as MarshalByRefObject;
        }
    }
}
