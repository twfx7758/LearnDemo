using Learn.CSharpForCLR.Learn;
using Learn.CSharpForCLR.LearnClass;
using Learn.SDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Learn.CSharpForCLR
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 测试代码
            //LearnAppDomain.MarshallingByRef();
            //AssemblyLoadReflect.LoadAssemblyAndShowPublicTypes();
            //AssemblyLoadReflect.LoadThirdSdk();
            //ATLProjectLib.FirstClass firstCls = new ATLProjectLib.FirstClass();
            //firstCls.Add(1, 2);
            //LearnSerialization.Serialization1();
            //LearnThread.ThreadMain2();
            #endregion

            LearnGCHandle.Go();

            Console.ReadLine();
            //强制退出进程
            //Environment.Exit(0);
        }


    }
}
