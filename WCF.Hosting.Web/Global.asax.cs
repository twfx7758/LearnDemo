using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WCF.SDK;
using WCF.Service;

namespace WCF.Hosting.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.Register<CalculatorService>("calculatorservice");
        }
    }
}