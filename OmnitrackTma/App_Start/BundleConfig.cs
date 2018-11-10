using System.Web;
using System.Web.Optimization;

namespace OmnitrackTma
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*_______________________________JS__________________________*/
            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/css/bootstrap.min.css",
                     "~/Content/fonts/css/font-awesome.min.css",
                     "~/Content/css/animate.min.css",
                     "~/Content/css/custom.css",
                     "~/Content/css/icheck/flat/green.css"
                     ));
   
            
              bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Content/js/jquery.min.js",
                        "~/Content/js/jquery-1.8.2.min.js"
                        ));
           

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //            "~/Content/js/bootstrap.min.js",
            //            "~/Content/js/progressbar/bootstrap-progressbar.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/chart").Include(
            //           "~/Content/js/chartjs/chart.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/icheck").Include(
            //         "~/Content/js/icheck/icheck.min.js"));

            //bundles.Add(new ScriptBundle("~/bundles/custom").Include(
            //        "~/Content/js/custom.js",
            //         "~/Content/js/nicescroll/jquery.nicescroll.min.js",
            //          "~/Content/js/jquery.min.js",
            //          "~/Content/js/jquery-1.8.2.min.js"));
            /*_______________________________JS__________________________*/

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));



              bundles.Add(new ScriptBundle("~/Content/js").Include(
                          "~/Content/js/bootstrap.min.js",
                          "~/Content/js/chartjs/chart.min.js",
                          "~/Content/js/progressbar/bootstrap-progressbar.min.js",
                          "~/Content/js/nicescroll/jquery.nicescroll.min.js",
                          "~/Content/js/icheck/icheck.min.js",
                          "~/Content/js/custom.js"

                          ));

            // Définissez EnableOptimizations sur False pour le débogage. Pour plus d'informations,
            // visitez http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
