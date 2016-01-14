using Learn.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int n = 0; n < 100; n++)
            {
                new PipeClient("localhost", "Request #" + n);
            }

            Console.ReadLine();
        }
    }
}
