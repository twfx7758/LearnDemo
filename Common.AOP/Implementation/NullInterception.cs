using Common.AOP.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AOP.Implementation
{
    public class NullInterception : IInterception
    {
        public void ExceptionHandle()
        {
            Console.WriteLine("Method Invoker ExceptionHandle()");
        }

        public void PostInvoke()
        {
            Console.WriteLine("Method Invoker PostInvoke()");
        }

        public void PreInvoke()
        {
            Console.WriteLine("Method Invoker PreInvoke()");
        }
    }
}
