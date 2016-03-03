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
        static readonly string connStr2 = ConfigurationManager.ConnectionStrings["DemoConnString2"].ConnectionString;
        static readonly string HMCCRMStr = ConfigurationManager.ConnectionStrings["HMCCRMRW"].ConnectionString;

        public void Transfer2(string accountFrom, string accountTo, float amount)
        {
            List<Action> list = new List<Action>();

            //同库同表不会提升为分布式事务
            //list.Add(() => Withdraw(accountFrom, amount));
            //list.Add(() => Deposite(accountTo, amount));
            //分布式事务，需要开启（DTC）windows服务
            //list.Add(() => TheSameMachine());//同一台机器，不同库，不同表，会提升为分布式事务（DTC）

            //跨库的情况，如果是一个connection连接，不会提升为分布式事务（在sql server2005以后版本），如果不是必定会提升为分布式事务
            list.Add(() => HMCCRMQuery2());
            list.Add(() => HMCCRMQuery());

            InvokeInTransactionScope(list);
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
            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "P_WITHDRAW", _param);
            
        }

        public void Deposite(string accountId, float amount)
        {
            SqlParameter[] _param ={
                new SqlParameter("@id",accountId),
                new SqlParameter("@amount",amount)
            };
            SqlHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "P_DEPOSIT", _param);
        }

        public void TheSameMachine()
        {
            string strSql = @" IF NOT EXISTS(SELECT * FROM [dbo].[Table_1] WHERE ID = 1000000)
                                   BEGIN
                                       RAISERROR ('帐户ID不存在',16,1)
                                   END
                               UPDATE dbo.Table_1 SET UserName='201' WHERE CustomerId=1000000";

            SqlHelper.ExecuteNonQuery(connStr2, CommandType.Text, strSql);
        }

        public void HMCCRMQuery()
        {
            string strSql = @" IF NOT EXISTS(SELECT * FROM [dbo].[CustomerInfo] WHERE CustomerId = 1000000)
                                   BEGIN
                                       RAISERROR ('帐户ID不存在',16,1)
                                   END
                               UPDATE dbo.CustomerInfo SET CityID=201 WHERE CustomerId=1000000";

            SqlHelper.ExecuteNonQuery(HMCCRMStr, CommandType.Text, strSql);
        }

        public void HMCCRMQuery2()
        {
            string strSql = "SELECT * FROM [dbo].[CustomerInfo] WHERE CustomerId = 1000000";

            SqlHelper.ExecuteNonQuery(HMCCRMStr, CommandType.Text, strSql);
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

        void InvokeInTransactionScope(List<Action> list)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                list.ForEach(a => a());
                transactionScope.Complete();
            }
        }
    }
}
