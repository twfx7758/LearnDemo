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
            throw new NotImplementedException();
        }

        public void PostInvoke()
        {
            throw new NotImplementedException();
        }

        public void PreInvoke()
        {
            throw new NotImplementedException();
        }
    }
}
