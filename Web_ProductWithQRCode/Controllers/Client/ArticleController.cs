using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web_ProductWithQRCode.Controllers.Client
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/

        public ActionResult Index()
        {
            ViewBag.Active = "active";
            return View();
        }
        public ActionResult ArticleByCate(string id)
        {
            return PartialView("~/Views/Client/article-cate.cshtml");
        }
    }
}
