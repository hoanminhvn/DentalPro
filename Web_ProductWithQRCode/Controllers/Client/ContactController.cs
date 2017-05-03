using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ProductWithQRCode.Controllers.Client
{
    public class ContactController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Active = "is-page";
            return View("~/Views/Client/Contact.cshtml");
        }
    }
}
