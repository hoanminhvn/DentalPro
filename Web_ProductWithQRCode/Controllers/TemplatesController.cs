using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ProductWithQRCode.Core.CustomModel;
using Web_ProductWithQRCode.Core.Helper;

namespace Web_ProductWithQRCode.Controllers
{
    public class TemplatesController : Controller
    {
        public ActionResult Admin()
        {
            return Redirect("/admin");
        }

        public ActionResult LoadPartialView(string folder, string view)
        {
            return View("~/Views/" + folder + "/" + view + ".cshtml");
        }
        public ActionResult LoadPartialViewSubFolder(string folder, string subfolder, string view)
        {
            return View("~/Views/" + folder + "/" + subfolder + "/" + view + ".cshtml");
        }

        public ActionResult LoadPartialViewWithId(string folder, string view, string id)
        {
            return View("~/Views/" + folder + "/" + view + ".cshtml");
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[Constants.USER_SESSION] = null;
            return Redirect("login");
        }
    }
}
