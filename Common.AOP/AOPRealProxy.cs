using Common.AOP.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Services;
using Common.AOP.Implementation;

namespace Common.AOP
{
    public class AOPRealProxy : RealProxy, IProxyDI
    {
        private MarshalByRefObject _target = null;
        private IInterception _interception = null;

        public AOPRealProxy(Type targetType, MarshalByRefObject target)
            :base(targetType)
        {
            _target = target;
            _interception = new NullInterception();
        }

        public override IMessage Invoke(IMessage msg)
        {
            IMethodReturnMessage methodReturnMessage = null;
            IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
            if (methodCallMessage != null)
            {
                IConstructionCallMessage constructionCallMessage = methodCallMessage as IConstructionCallMessage;
                if (constructionCallMessage != null)
                {
                    RealProxy defaultProxy = RemotingServices.GetRealProxy(_target);
                    defaultProxy.InitializeServerObject(constructionCallMessage);
                    methodReturnMessage = EnterpriseServicesHelper.CreateConstructionReturnMessage(constructionCallMessage, _target);
                }
                else
                {
                    _interception.PreInvoke();
                    try
                    {
                        methodReturnMessage = RemotingServices.ExecuteMessage(_target, methodCallMessage);
                    }
                    catch
                    {
                    }

                    if (methodReturnMessage.Exception != null)
                    {
                        _interception.ExceptionHandle();
                    }
                    else
                    {
                        _interception.PostInvoke();
                    }
                }
            }

            return methodReturnMessage;
        }

        #region IProxyDI Members
        public void InterceptionDI(IInterception interception)
        {
            _interception = interception;
        }
        #endregion
    }
}
