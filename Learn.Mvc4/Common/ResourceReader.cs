using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Mvc4
{
    public abstract class ResourceReader
    {
        public abstract string GetString(string name);
    }
}
