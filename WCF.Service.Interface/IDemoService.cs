using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF.Service.Interface
{
    [ServiceContract]
    public interface IDemoService
    {
        [OperationContract]
        IEnumerable<string> GetItems(string category);
    }
}
