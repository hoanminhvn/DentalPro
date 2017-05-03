using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Web_ProductWithQRCode
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/client/jquery").Include(
                        "~/Assets/client/lib/jquery/jquery-1.11.2.min.js",
                        "~/Assets/client/lib/bootstrap/js/bootstrap.min.js",
                        "~/Assets/client/lib/select2/js/select2.min.js",
                        "~/Assets/client/lib/jquery.bxslider/jquery.bxslider.min.js",
                        "~/Assets/client/lib/owl.carousel/owl.carousel.min.js",
                        "~/Assets/client/lib/jquery.countdown/jquery.countdown.min.js",
                        "~/Assets/client/lib/fancyBox/jquery.fancybox.js",
                        "~/Assets/client/lib/jquery.elevatezoom.js",
                        "~/Assets/client/js/theme-script.js",
                        "~/Assets/client/js/equalheight.js"));

            bundles.Add(new ScriptBundle("~/bundles/admin/jquery").Include(
                        "~/Assets/admin/js/jquery.min.js",
                        "~/Assets/admin/js/angularjs/angular.min.js",
                        "~/Assets/admin/js/angularjs/angular-route.min.js",
                        "~/Assets/admin/js/angularjs/angular-pagination.js",
                        "~/Assets/admin/js/angularjs/angular-flash.min.js",
                        "~/Assets/admin/js/angularjs/angular-switcher.js",
                        "~/Assets/admin/js/angularjs/angular-md5.js",
                        "~/Assets/admin/js/angularjs/angular-autocomplete.js",
                        "~/Assets/admin/js/angularjs/chart.js",
                        "~/Assets/admin/js/angularjs/angular-chart.min.js",
                        "~/Assets/admin/js/angularjs/angular-ckeditor.min.js",
                        "~/Assets/admin/js/angularjs/qrcode.js",
                        "~/Assets/admin/js/angularjs/angular-qr.min.js",
                        "~/Assets/admin/plugins/ckeditor/ckeditor.js",
                        "~/Assets/admin/plugins/ckfinder/ckfinder.js"
                    ));

            bundles.Add(new StyleBundle("~/bundles/admin/css").Include(
                      "~/Assets/admin/plugins/morris/morris.css",
                      "~/Assets/admin/css/bootstrap.min.css",
                      "~/Assets/admin/css/core.css",
                      "~/Assets/admin/css/components.css",
                      "~/Assets/admin/css/icons.css",
                      "~/Assets/admin/css/pages.css",
                      "~/Assets/admin/css/responsive.css",
                      "~/Assets/admin/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css",
                      "~/Assets/admin/css/angular-switcher.css",
                      "~/Assets/admin/plugins/custombox/css/custombox.css",
                      "~/Assets/admin/css/nv.d3.css"));

            bundles.Add(new StyleBundle("~/bundles/client/css").Include(
                      "~/Assets/client/lib/bootstrap/css/bootstrap.min.css",
                      "~/Assets/client/lib/font-awesome/css/font-awesome.min.css",
                      "~/Assets/client/lib/Linearicons/css/demo.css",
                      "~/Assets/client/lib/select2/css/select2.min.css",
                      "~/Assets/client/lib/jquery.bxslider/jquery.bxslider.css",
                      "~/Assets/client/lib/owl.carousel/owl.carousel.css",
                      "~/Assets/client/lib/fancyBox/jquery.fancybox.css",
                      "~/Assets/client/css/reset.css",
                      "~/Assets/client/css/index9.css",
                      "~/Assets/client/css/responsive.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}