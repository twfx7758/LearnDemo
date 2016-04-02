using Common.AOP.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Common.AOP.Implementation
{
    public class TransactionAOP : IInterception
    {
        TransactionScope tran = null;

        public void ExceptionHandle()
        {
            tran.Dispose();
        }

        public void PostInvoke()
        {
            tran.Complete();
            tran.Dispose();
        }

        public void PreInvoke()
        {
            tran = new TransactionScope();
        }
    }
}
