using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learn.CSharpForCLR.LearnClass
{
    /*
     * metahost.h(ICLRMetaHost CLRCreateInstance方法)
     * ICLRRuntimeInfo ICLRMetaHost.GetRuntime
     * ICLRRuntimeHost ICLRRuntimeInfo.GetInterface
     * ICLRRuntimeHost接口可以做很多事^_^
     一、寄宿(hosting)
         寄宿允许任务应用程序都能利用CLR的功能。
         特点要指出的是，它允许现有的应用程序至少部分使用托管代码编写。
         寄宿还为应用程序提供了通过编程来进行自定义和扩展的功能。
     二、AppDomain
         允许可扩展性意味着第三方的代码可在你的进程中运行。将第三方的DLL加载到进程中意味着危险。
         第三方DLL可能破坏应用程序的数据结构和代码。还可以通过应用程序的安全上下文来访问它本来无权访问的资源。
         AppDomain就是解决这些安全问题的。
     三、应用程序加载CLR后，在进程结束前，不能卸载。CLR不支持从AppDomain中卸载一个程序集的能力。但是，可以告诉CLR卸载一个AppDomain。
     四、CLR COM服务器初始化后，会创建一个AppDomain,这个是默认AppDomain，这个默认AppDomain只有在进程结束才会被销毁。
    */
    public class LearnAppDomain
    {
        public static void MarshallingByRef()
        {
            //获取对AppDomain的一个引用（“调用线程”当前正在该AppDomain中执行）
            AppDomain adCallingThreadDomain = Thread.GetDomain();
            
            //每个AppDomain 都被赋予了一个友好的字符串名称（有利于调试）
            String callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine("Default AppDomain's friendly name={0}", callingDomainName);

            //获取显示我们的AppDomain中包含了“Main”方法的程序集
            String exeAssembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main Assembly={0}", exeAssembly);

            AppDomain ad2 = null;
            //*** DEMO 1:使用Marshal-by-Reference进行跨AppDomain通信 ***
            Console.WriteLine("{0}Demo #1", Environment.NewLine);

            //新建一个AppDomain(安全性和配置匹配于当前AppDomain)
            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            MarshalByRefType mbrt = null;

            //将我们的程序集加载到新的AppDomain中，构造一个对象，把它
            //封送回我们的AppDomain(实际得到对一个代理的引用)
            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "MarshalByRefType");

            Console.WriteLine("Type={0}", mbrt.GetType()); //CLR在类型上撒谎了

            //证明得到的是对一个代理对象的引用
            Console.WriteLine("Is Proxy={0}", RemotingServices.IsTransparentProxy(mbrt));

            //看起来像是在MarshalByRefObject上调用一个方法，实则不然
            //我们是在代理类型上调用一个方法，代理使纯种转到拥有对象的
            //那个AppDomain，并存真实的对象上调用这个方法。
            mbrt.SomeMethod();

            //卸载新的AppDomain
            AppDomain.Unload(ad2);

            //mbrt引用一个有效的代理对象，代理对象引用一个无效的AppDomain

            try
            {
                mbrt.SomeMethod();
                Console.WriteLine("Successful call.");
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("Failed call.");
            }
        }

        public static void MarshallingByVal()
        {
            //获取对AppDomain的一个引用（“调用线程”当前正在该AppDomain中执行）
            AppDomain adCallingThreadDomain = Thread.GetDomain();

            //每个AppDomain 都被赋予了一个友好的字符串名称（有利于调试）
            String callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine("Default AppDomain's friendly name={0}", callingDomainName);

            //获取显示我们的AppDomain中包含了“Main”方法的程序集
            String exeAssembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main Assembly={0}", exeAssembly);

            AppDomain ad2 = null;
            //*** DEMO 1:使用Marshal-by-Reference进行跨AppDomain通信 ***
            Console.WriteLine("{0}Demo #2", Environment.NewLine);

            //新建一个AppDomain(安全性和配置匹配于当前AppDomain)
            ad2 = AppDomain.CreateDomain("AD #2", null, null);
            MarshalByRefType mbrt = null;

            //将我们的程序集加载到新的AppDomain中，构造一个对象，把它
            //封送回我们的AppDomain(实际得到对一个代理的引用)
            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "MarshalByRelType");

            MarshalByValType mbvt = mbrt.MethodWithReturn();

            //证明得到的是对一个代理对象的引用
            Console.WriteLine("Is Proxy={0}", RemotingServices.IsTransparentProxy(mbvt));

            //看起来像是在MarshalByValType上调用一个方法，实际上也是。
            Console.WriteLine("Returned object created " + mbvt.ToString());

            //卸载新的AppDomain
            AppDomain.Unload(ad2);

            //mbvt引用一个有效的对象，卸载AppDomain没有影响

            try
            {
                Console.WriteLine("Returned object created " + mbvt.ToString());
                Console.WriteLine("Successful call.");
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("Failed call.");
            }
        }

        public static void MarshallingByable()
        {
            //获取对AppDomain的一个引用（“调用线程”当前正在该AppDomain中执行）
            AppDomain adCallingThreadDomain = Thread.GetDomain();

            //每个AppDomain 都被赋予了一个友好的字符串名称（有利于调试）
            String callingDomainName = adCallingThreadDomain.FriendlyName;
            Console.WriteLine("Default AppDomain's friendly name={0}", callingDomainName);

            //获取显示我们的AppDomain中包含了“Main”方法的程序集
            String exeAssembly = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("Main Assembly={0}", exeAssembly);

            AppDomain ad2 = null;
            //*** DEMO 1:使用Marshal-by-Reference进行跨AppDomain通信 ***
            Console.WriteLine("{0}Demo #3", Environment.NewLine);

            //新建一个AppDomain(安全性和配置匹配于当前AppDomain)
            ad2 = AppDomain.CreateDomain("AD #3", null, null);
            MarshalByRefType mbrt = null;

            //将我们的程序集加载到新的AppDomain中，构造一个对象，把它
            //封送回我们的AppDomain(实际得到对一个代理的引用)
            mbrt = (MarshalByRefType)ad2.CreateInstanceAndUnwrap(exeAssembly, "MarshalByRelType");

            //抛出异常
            NonMarshalableType nmt = mbrt.MethodWithReturn(callingDomainName);
        }
    }

    //该类的实例可跨越AppDomain的边界“按引用封送”
    public sealed class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByRefType()
        {
            Console.WriteLine("{0} ctor running in {1}", this.GetType().ToString(),
                Thread.GetDomain().FriendlyName);
        }

        public void SomeMethod()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
        }

        public MarshalByValType MethodWithReturn()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
            MarshalByValType t = new MarshalByValType();
            return t;
        }

        public NonMarshalableType MethodWithReturn(String callingDomainName)
        {
            //注意：callingDomainName是可序列化的
            Console.WriteLine("Calling from '{0}' to '{1}'.", callingDomainName,
                Thread.GetDomain().FriendlyName);
            NonMarshalableType t = new NonMarshalableType();
            return t;
        }
    }

    [Serializable]
    public sealed class MarshalByValType : Object
    {
        private DateTime m_CreationTime = DateTime.Now; //注意：DateTime是可序列化的

        public MarshalByValType()
        {
            Console.WriteLine("{0} ctor running in {1}, Created on {2:D}",
                this.GetType().ToString(),
                Thread.GetDomain().FriendlyName,
                m_CreationTime);
        }

        public override String ToString()
        {
            return m_CreationTime.ToLongDateString();
        }
    }

    public sealed class NonMarshalableType : Object
    {
        public NonMarshalableType()
        {
            Console.WriteLine("Executing in " + Thread.GetDomain().FriendlyName);
        }
    }
}
