using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Mvc4
{
    public class DefaultResourceReader : ResourceReader
    {
        public override string GetString(string name)
        {
            return Resources.Resources.ResourceManager.GetString(name);
        }
    }
}
