using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Learn.Mvc4
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //依赖注入
            NinjectDependencyResolver dependencyResovler = new NinjectDependencyResolver();
            dependencyResovler.Register<ResourceReader, DefaultResourceReader>();
            DependencyResolver.SetResolver(dependencyResovler);

            //自定义ViewEngines
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new SimpleRazorViewEngine());
        }

        protected void Application_BeginRequest()
        {
            HttpContextBase contextWrapper = new HttpContextWrapper(HttpContext.Current);
            string culture = RouteTable.Routes.GetRouteData(contextWrapper).Values["culture"] as string;
            if (!string.IsNullOrEmpty(culture))
            {
                try
                {
                    CultureInfo cultureInfo = new CultureInfo(culture);
                    Thread.CurrentThread.CurrentCulture = cultureInfo;
                    Thread.CurrentThread.CurrentUICulture = cultureInfo;
                }
                catch { }
            }
        }
    }
}
