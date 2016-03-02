using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace WCF.Trans
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new CommittableTransaction().TransactionInformation.LocalIdentifier);
            Console.WriteLine(new CommittableTransaction().TransactionInformation.LocalIdentifier);
            Console.WriteLine(new CommittableTransaction().TransactionInformation.LocalIdentifier);

            Console.ReadLine();
        }
    }
}
