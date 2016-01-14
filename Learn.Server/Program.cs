using Learn.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int n = 0; n > Environment.ProcessorCount; n++)
            {
                new PipeServer();
            }

            Console.WriteLine("Press <Enter> to terminate this server application.");
            Console.ReadLine();
        }
    }
}
