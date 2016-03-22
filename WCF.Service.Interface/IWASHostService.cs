﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCF.Service.Interface
{
    [ServiceContract(Namespace = "http://www.kf.com/WASHost", Name = "WasHost")]
    public interface IWASHostService
    {
        [OperationContract]
        string HelloWCF();
    }
}
