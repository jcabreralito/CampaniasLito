using System.Web;
using System.Web.Optimization;

namespace CampaniasLito
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      //"~/Content/css/materialize/css/materialize.min.css",
                      "~/Content/vendor/bootstrap/css/bootstrap.min.css",
                      "~/Content/vendor/metisMenu/metisMenu.min.css",
                      "~/Content/dist/css/sb-admin-2.css",
                      "~/Content/vendor/morrisjs/morris.css",
                      "~/Content/bootstrap-datetimepicker.css",
                      "~/Content/css/Generales.css",
                      "~/Content/css/Estilos.css",
                      "~/Content/css/Vistas.css",
                      "~/Content/site.css"));
            //"~/Content/css/inputfile.css"


            bundles.Add(new StyleBundle("~/bundles/js").Include(
                      "~/Content/vendor/jquery/jquery.min.js",
                      //"~/Content/css/materialize/js/materialize.min.js",
                      "~/Content/vendor/bootstrap/js/bootstrap.min.js",
                      "~/Content/vendor/metisMenu/metisMenu.min.js",
                      //"~/Content/vendor/raphael/raphael.min.js",
                      //"~/Content/vendor/morrisjs/morris.min.js",
                      //"~/Content/data/morris-data.js",
                      "~/Content/dist/js/sb-admin-2.js",
                      "~/Scripts/CampaniasLito.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/bootstrap-datetimepicker.js",
                      //"~/Scripts/fileupload.js",
                      //"~/Content/js/inputfile-custom.js",
                      "~/Scripts/fileupload.js"));

        }
    }
}
