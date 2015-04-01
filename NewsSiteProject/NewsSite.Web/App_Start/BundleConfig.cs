namespace NewsSite.Web
{
    using System.Web;
    using System.Web.Optimization;

    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScripts(bundles);
            RegisterStyles(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterScripts(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include(
                        "~/Scripts/gridmvc.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.js",
                        "~/Scripts/moment-datepicker.js",
                        "~/Scripts/moment-with-locales.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Content/js/app.js",
                        "~/Content/js/url-builder.js",
                        "~/Content/js/http-requester.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").IncludeDirectory(
                        "~/Content/js", "*.js", true));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
        }

        private static void RegisterStyles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.cerulean.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/gridmvc").Include(
                      "~/Content/Gridmvc.css"));

            bundles.Add(new StyleBundle("~/Content/custom-css").IncludeDirectory(
                       "~/Content/css", "*.css", true));
        }
    }
}
