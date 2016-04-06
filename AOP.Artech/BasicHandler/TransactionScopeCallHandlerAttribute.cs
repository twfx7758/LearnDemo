using AOP.Artech.Abstract;
using AOP.Artech.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOP.Artech.BasicHandler
{
    public class TransactionScopeCallHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateCallHandler()
        {
            return new TransactionScopeCallHandler() { Ordinal = this.Ordinal, ReturnIfError = this.ReturnIfError };
        }
    }
}
