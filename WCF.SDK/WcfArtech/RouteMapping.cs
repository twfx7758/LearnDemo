using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.SDK
{
    public class RouteMapping
    {
        public string Address { get; private set; }
        public Type ServiceType { get; private set; }

        public RouteMapping(string address, Type serviceType)
        {
            this.Address = address;
            this.ServiceType = serviceType;
        }
    }
}
