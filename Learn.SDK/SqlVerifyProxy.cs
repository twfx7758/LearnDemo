using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace Learn.SDK
{
    public class SqlVerifyProxy : RealProxy
    {
        MarshalByRefObject _target = null;

        public SqlVerifyProxy(Type type, MarshalByRefObject target)
            : base(type)
        {
            this._target = target;
        }

        //覆写Invoke，处理RealProxy截获的各种消息,
        //此种方式最简捷，但不能截获远程对象的激活,好在我们并不是真的要Remoting
        public override IMessage Invoke(IMessage msg)
        {
            //throw new NotImplementedException();
            IMethodMessage call = (IMethodMessage)msg;
            IConstructionCallMessage ctr = call as IConstructionCallMessage;
            IMethodReturnMessage back = null;
            //构造函数，只有ContextBoundObject(Inherit from MarshalByRefObject)对象才能截获构造函数
            if (ctr != null)
            {
                RealProxy defaultProxy = RemotingServices.GetRealProxy(_target);
                //如果不做下面这一步，_target还是一个没有真正实例化被代理对象的透明代理
                //这样的话，会导致没有真正构建对象
                defaultProxy.InitializeServerObject(ctr);
                //本类是一个RealProxy，它可通过GetTransparentProxy函数得到透明代理
                back = EnterpriseServicesHelper.CreateConstructionReturnMessage(ctr,
                    (MarshalByRefObject)GetTransparentProxy());
            }
            //MarshalByRefObject对象就可截获普通的调用消息
            //MarshalByRefObject对象告诉编译器，不能将其内部简单的成员函数优化成内联代码
            //这样才能保证函数调用都能截获
            else
            {
                IDictionary<string, object> dic = new Dictionary<string, object>();
                //dic = actionContext
            }

            return back;
        }
    }
}
