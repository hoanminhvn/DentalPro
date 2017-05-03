using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_ProductWithQRCode.Core.Entity;
using Web_ProductWithQRCode.Core.Models;

namespace Web_ProductWithQRCode.Controllers.Client
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductCategory(string id)
        {
            var product_detail = new ProductsModel().GetByCateMeta(id);
            return PartialView("~/Views/Client/product-detail.cshtml", product_detail);
        }

        public ActionResult ProductDetail(string id)
        {
            ViewBag.Active = "none";
            var categoriesModel = new CategoriesModel();
            var productsModel = new ProductsModel();

            var listRelatedProduct = new HashSet<Products>();
            var listCate = new List<Categories>();

            var product_detail = productsModel.GetByMeta(id);


            ViewBag.Product = product_detail;

            //Get list categories of product
            string[] arr_cateId = product_detail.CateID.Split(':');
            
            int countList = arr_cateId.Length -1;
            for (int i = 1; i < countList; i++)
            {
                int cateId = Convert.ToInt32(arr_cateId[i]);
                var category = categoriesModel.GetById(cateId);
                if(category != null)
                    listCate.Add(category);
                var listProducts = productsModel.GetByCateId(cateId);
                AddRelatedProduct(listProducts, ref listRelatedProduct);
                
            }
            listRelatedProduct.RemoveWhere(x => x.ID == product_detail.ID);
            ViewBag.Categories = listCate;

            //get list image of product
            ViewBag.ListImage = product_detail.ListImage.Split(':');
            //Get list tag of product
            ViewBag.ListTag = product_detail.Tag.Split(':');

            ViewBag.RelatedProduct = listRelatedProduct;
            return PartialView("~/Views/Client/product-detail.cshtml");
        }
        protected void AddRelatedProduct(List<Products> listProducts, ref HashSet<Products> listRelatedProduct)
        {
            foreach (Products item in listProducts)
            {
                listRelatedProduct.Add(item);
            }
        }
    }
}
