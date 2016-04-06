﻿using AOP.Artech;
using AOP.Artech.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AOP.Artech.BasicHandler
{
    public class TransactionScopeCallHandler : CallHandlerBase
    {
        public override object PreInvoke(InvocationContext context)
        {
            return new TransactionScope();
        }

        public override void PostInvoke(InvocationContext context, object correlationState)
        {
            TransactionScope transactionScope = (TransactionScope)correlationState;
            if (context.Reply.Exception == null)
            {
                transactionScope.Complete();
            }
            transactionScope.Dispose();
        }
    }
}
