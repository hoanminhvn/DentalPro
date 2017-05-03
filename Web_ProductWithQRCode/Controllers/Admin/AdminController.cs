using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ProductWithQRCode.Core.CustomModel;
using Web_ProductWithQRCode.Core.Helper;

namespace Web_ProductWithQRCode.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            var session = (AccountSession)Session[Constants.USER_SESSION];
            if (session == null)
                return Redirect("/login");
            return View();
        }
        public ActionResult Home()
        {
            var session = (AccountSession)Session[Constants.USER_SESSION];
            if (session == null)
                return Redirect("login");
            return View();
        }
        
        public ActionResult ResetPass()
        {
            return View();
        }
        public ActionResult ChangePass()
        {
            return View();
        }
    }
}
