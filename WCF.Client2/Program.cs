using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            new ServiceInvoker().Add(10, 12);

            Console.ReadLine();
        }
    }
}
