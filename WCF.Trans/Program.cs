using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Trans
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(new CommittableTransaction().TransactionInformation.LocalIdentifier);
            //Console.WriteLine(new CommittableTransaction().TransactionInformation.LocalIdentifier);
            //Console.WriteLine(new CommittableTransaction().TransactionInformation.LocalIdentifier);

            TbAccountDal dal = new TbAccountDal();
            string accountFoo = "Foo";
            string nonExistentAccount = Guid.NewGuid().ToString();
            //输出转帐之前的余额
            Console.WriteLine("帐户\"{0}\"的当前余额为：￥{1}", accountFoo, dal.GetBalance(accountFoo));
            //开始转帐    
            try
            {
                dal.Transfer2(accountFoo, nonExistentAccount, 1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("转帐失败，错误信息：{0}", ex.Message);
            }
            //输出转帐后的余额
            Console.WriteLine("帐户\"{0}\"的当前余额为：￥{1}", accountFoo, dal.GetBalance(accountFoo));

            Console.ReadLine();
        }
    }
}
