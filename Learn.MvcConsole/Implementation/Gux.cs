using Learn.MvcConsole.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn.MvcConsole.Implementation
{
    public class Gux : IGux
    {
        public IBar Bar
        {
            get;
            private set;
        }

        public IBaz Baz
        {
            get;
            private set;
        }

        public IFoo Foo
        {
            get;
            private set;
        }

        public Gux(IFoo foo)
        {
            System.Console.WriteLine("Gux(IFoo)");
        }

        public Gux(IFoo foo, IBar bar)
        {
            System.Console.WriteLine("Gux(IFoo, IBar)");
        }

        public Gux(IFoo foo, IBar bar, IBaz baz)
        {
            System.Console.WriteLine("Gux(IFoo, IBar, IBaz)");
        }
    }
}
