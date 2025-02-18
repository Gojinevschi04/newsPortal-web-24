using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Gastronique.Web.App_Start
{
     public class BundleConfig
     {

          // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
          public static void RegisterBundles(BundleCollection bundles)
          {
               // Bootstrap
               bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                   "~/Scripts/bootstrap.min.js",
                   "~/Scripts/bootstrap.min.js.map",
                   "~/Scripts/bootstrap.bundle.min.js",
                   "~/Scripts/bootstrap.bundle.min.js.map"
               ));

               // jQuery
               bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
                   "~/Scripts/jquery.min.js",
                   "~/Scripts/jquery.min.map",
                   "~/Scripts/jquery.slim.min.map",
                   "~/Scripts/jquery.slim.min.js",
                   "~/Scripts/jquery.dataTables.min.js",
                   "~/Scripts/jquery.easing.min.js",
                   "~/Scripts/jquery.easing.compatibility.min.js",
                   "~/Scripts/jquery.slim.min.js"
               ));

               bundles.Add(new ScriptBundle("~/bundles/main/js").Include(
                   "~/Scripts/sb-admin-2.min.js"
               ));

               // Chart
               bundles.Add(new ScriptBundle("~/bundles/demo/js").Include(
                   "~/Scripts/demo/chart-area-demo.js",
                   "~/Scripts/demo/chart-bar-demo.js",
                   "~/Scripts/demo/chart-pie-demo.js",
                   "~/Scripts/demo/datatables-demo.js"
               ));

               bundles.Add(new ScriptBundle("~/bundles/chart-js/js").Include(
                   "~/Scripts/Chart.min.js",
                   "~/Scripts/Chart.bundle.min.js"
               ));

               // Font Awesome
               bundles.Add(new ScriptBundle("~/bundles/font-awesome/js").Include(
                   "~/Scripts/font-awesome/all.min.js",
                   "~/Scripts/font-awesome/brands.min.js",
                   "~/Scripts/font-awesome/conflict-detection.min.js",
                   "~/Scripts/font-awesome/fontawesome.min.js",
                   "~/Scripts/font-awesome/regular.min.js",
                   "~/Scripts/font-awesome/solid.min.js",
                   "~/Scripts/font-awesome/v4-shims.min.js"
               ));

               // SB Admin 2
               bundles.Add(new StyleBundle("~/bundles/main/css").Include(
                       "~/Content/sb-admin-2.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/dataTables.bootstrap4.min.css", new CssRewriteUrlTransform()
                   ));

               // Font Awesome
               bundles.Add(new StyleBundle("~/bundles/font-awesome/css").Include(
                       "~/Content/font-awesome/all.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/font-awesome/brands.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/font-awesome/fontawesome.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/font-awesome/regular.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/font-awesome/solid.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/font-awesome/svg-with-js.min.css", new CssRewriteUrlTransform())
                   .Include("~/Content/font-awesome/v4-shims.min.css", new CssRewriteUrlTransform())
               );
          }
     }
}