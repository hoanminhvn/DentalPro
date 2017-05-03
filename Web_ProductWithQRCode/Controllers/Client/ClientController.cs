using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ProductWithQRCode.Core.Models;

namespace Web_ProductWithQRCode.Controllers.Client
{
    public class ClientController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Active = "is-home";
            return View();
        }
        public ActionResult RecentProducts()
        {
            var recent_products = new ProductsModel().GetRecentProduct();
            return PartialView(recent_products);
        }
        public ActionResult BestSaleProducts()
        {
            var recent_products = new ProductsModel().GetRecentProduct();
            return PartialView(recent_products);
        }
        public ActionResult ProductDetail(string meta)
        {
            var product_detail = new ProductsModel().GetByMeta(meta);
            return PartialView(product_detail);
        }
    }
}
