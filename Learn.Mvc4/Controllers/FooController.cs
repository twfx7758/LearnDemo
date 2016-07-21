using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Learn.Mvc4.Controllers
{
    public class FooController : Controller
    {
        public ActionResult Action1()
        {
            return View();
        }

        public ActionResult Action2()
        {
            var context = SynchronizationContext.Current;
            return View();
        }
    }
}