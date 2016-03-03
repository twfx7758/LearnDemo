using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCF.Service.Interface;

namespace WCF.Service
{
    public class WASHostService : IWASHostService
    {
        public string HelloWCF(string str)
        {
            return string.Format("Get message from client is : {0}", str);
        }
    }
}
