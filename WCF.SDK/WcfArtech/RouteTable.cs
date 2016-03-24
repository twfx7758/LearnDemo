using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.SDK
{
    public class RouteTable : Collection<RouteMapping>
    {
        public static RouteTable Routes { get; private set; }

        static RouteTable()
        {
            Routes = new RouteTable();
        }

        public Type Find(string address)
        {
            RouteMapping routeMapping = (from route in this
                                         where string.Compare(route.Address, address, true) == 0
                                         select route).FirstOrDefault();

            return null == routeMapping ? null : routeMapping.ServiceType;
        }

        public void Register<T>(string address)
        {
            this.Add(new RouteMapping(address, typeof(T)));
        }
    }
}
