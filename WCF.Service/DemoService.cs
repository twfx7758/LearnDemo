using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Service.Interface;
using System.ServiceModel;

namespace WCF.Service
{
    public class DemoService : IDemoService
    {
        public IEnumerable<string> GetItems(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                throw new FaultException("Invalid category");
            }

            yield return "Foo";
            yield return "Bar";
            yield return "Baz";
        }
    }
}
