using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learn.MvcConsole.Interface
{
    public interface IGux
    {
        IFoo Foo { get; }
        IBar Bar { get; }
        IBaz Baz { get; }
    }
}
