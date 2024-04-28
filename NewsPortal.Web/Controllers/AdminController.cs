using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NewsPortal.Web.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Users()
        {
            return View();
        }
    }
}