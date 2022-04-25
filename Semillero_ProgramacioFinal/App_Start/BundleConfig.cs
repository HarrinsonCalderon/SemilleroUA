using System.Web;
using System.Web.Optimization;

namespace Semillero_ProgramacioFinal
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Home/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/animate").Include(
                      "~/Content/Home/animate.css"));

            bundles.Add(new StyleBundle("~/Content/bxslider").Include(
                      "~/Content/Home/jquery.bxslider.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                     "~/Content/Home/font-awesome.min.css",
                     "~/Content/Home/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/site").Include(
                      "~/Content/Home/Site.css"));

            bundles.Add(new StyleBundle("~/Content/overwrite").Include(
                      "~/Content/Home/overwrite.css"));

            bundles.Add(new StyleBundle("~/Content/Formularios").Include(
                      "~/Content/Home/formulario.css"));
          
             
        }
    }
}
