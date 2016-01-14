using Learn.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdPartySDK
{
    public sealed class AddIn_A : IAddIn
    {
        public AddIn_A()
        { }
        public string DoSomething(int x)
        {
            return "AddIn_A: " + x.ToString();
        }
    }

    public sealed class AddIn_B : IAddIn
    {
        public AddIn_B()
        { }
        public string DoSomething(int x)
        {
            return "AddIn_B: " + x.ToString();
        }
    }
}
