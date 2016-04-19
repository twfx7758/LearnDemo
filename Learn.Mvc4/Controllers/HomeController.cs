using Learn.Mvc4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Compilation;
using System.Web.Mvc;

namespace Learn.Mvc4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public void Index()
        {
            ViewData.Model = new Contact {
                Name = "屈文斌",
                PhoneNo = "13718312531",
                Email = "980499097@qq.com"
            };
            SimpleRazorView view = new SimpleRazorView("~/Views/Home/Index.cshtml");
            ViewContext viewContext = new ViewContext(ControllerContext, view, ViewData, TempData, Response.Output);
            view.Render(viewContext, viewContext.Writer);
        }

        public void Action1()
        {
            Response.Write(BuildManager.GetCompiledType("~/Views/Foo/Action1.cshtml") + "<br />");
            Response.Write(BuildManager.GetCompiledType("~/Views/Foo/Action2.cshtml") + "<br />");
            Response.Write(BuildManager.GetCompiledType("~/Views/Bar/Action1.cshtml") + "<br />");
            Response.Write(BuildManager.GetCompiledType("~/Views/Bar/Action2.cshtml") + "<br />");
        }

        public ActionResult Action2()
        {
            return View();
        }
    }
}