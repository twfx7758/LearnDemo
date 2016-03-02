using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using WCF.SDK.Common;
using System.Transactions;
using System.Threading;

namespace WCF.Trans
{
    public class TbAccountDal
    {
        static readonly string connStr = ConfigurationManager.ConnectionStrings["DemoConnString"].ConnectionString;

        public void Transfer2(string accountFrom, string accountTo, float amount)
        {
            Withdraw(accountFrom, amount);
            Deposite(accountTo, amount);
        }

        public void Transfer(string accountFrom, string accountTo, float amount)
        {
            Transaction originalTransaction = Transaction.Current;
            CommittableTransaction transaction = new CommittableTransaction();
            try
            {
                Transaction.Current = transaction;
                ThreadPool.QueueUserWorkItem(state =>
                {
                    Transaction.Current = state as DependentTransaction;
                    try
                    {
                        Withdraw(accountFrom, amount);
                        Deposite(accountTo, amount);
                        (state as DependentTransaction).Complete();
                    }
                    catch (Exception ex)
                    {
                        Transaction.Current.Rollback(ex);
                    }
                    finally
                    {
                        (state as IDisposable).Dispose();
                        Transaction.Current = null;
                    }
                }, Transaction.Current.DependentClone(DependentCloneOption.BlockCommitUntilComplete));

                transaction.Commit();
            }
            catch (TransactionAbortedException ex)
            {
                transaction.Rollback(ex);
                Console.WriteLine("转帐失败，错误信息：{0}", ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                transaction.Rollback(ex);
                throw;
            }
            finally
            {
                Transaction.Current = originalTransaction;
                transaction.Dispose();
            }
        }

        public void Withdraw(string accountId, float amount)
        {
            SqlParameter[] _param ={
                new SqlParameter("@id",accountId),
                new SqlParameter("@amount",amount)
            };
            InvokeInTransaction(()=> SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "P_WITHDRAW", _param));
            
        }

        public void Deposite(string accountId, float amount)
        {
            SqlParameter[] _param ={
                new SqlParameter("@id",accountId),
                new SqlParameter("@amount",amount)
            };
            InvokeInTransaction(() => SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "P_DEPOSIT", _param));
        }
            

        public float GetBalance(string accountId)
        {
            SqlParameter[] _param ={
                new SqlParameter("@id",accountId)
            };
            var ret = SqlHelper.ExecuteScalar(connStr, CommandType.StoredProcedure, "P_GET_BALANCE_BY_ID", _param);
            return Convert.ToSingle(ret);
        }

        void InvokeInTransaction(Action action)
        {
            Transaction originalTransaction = Transaction.Current;
            CommittableTransaction committableTransaction = null;
            DependentTransaction dependentTransaction = null;
            if (null == Transaction.Current)
            {
                committableTransaction = new CommittableTransaction();
                Transaction.Current = committableTransaction;
            }
            else
            {
                dependentTransaction = Transaction.Current.DependentClone(DependentCloneOption.RollbackIfNotComplete);
                Transaction.Current = dependentTransaction;
            }

            try
            {
                action();
                if (null != committableTransaction)
                {
                    committableTransaction.Commit();
                }

                if (null != dependentTransaction)
                {
                    dependentTransaction.Complete();
                }
            }
            catch (Exception ex)
            {
                Transaction.Current.Rollback(ex);
                throw;
            }
            finally
            {
                Transaction transaction = Transaction.Current;
                Transaction.Current = originalTransaction;
                transaction.Dispose();
            }
        }

        void InvokeInTransactionScope(Action action)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                action();
                transactionScope.Complete();
            }
        }
    }
}
