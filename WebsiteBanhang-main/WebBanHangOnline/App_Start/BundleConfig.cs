using System.Web;
using System.Web.Optimization;

namespace WebBanHangOnline
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // jQuery core
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"
            ));

            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*",
                "~/Scripts/jquery.validate.unobtrusive*"
            ));

            // Modernizr (dùng cho feature detection)
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"
            ));

            // Bootstrap core (dùng nếu không xài bản bootstrap.min.js trong assets)
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"
            ));

            // ✅ Bundle CSS: toàn bộ style
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/assets/css/bootstrap.min.css",
                "~/Content/assets/css/open-iconic-bootstrap.min.css",
                "~/Content/assets/css/animate.css",
                "~/Content/assets/css/owl.carousel.min.css",
                "~/Content/assets/css/owl.theme.default.min.css",
                "~/Content/assets/css/magnific-popup.css",
                "~/Content/assets/css/aos.css",
                "~/Content/assets/css/ionicons.min.css",
                "~/Content/assets/css/bootstrap-datepicker.css",
                "~/Content/assets/css/jquery.timepicker.css",
                "~/Content/assets/css/flaticon.css",
                "~/Content/assets/css/icomoon.css",
                "~/Content/assets/plugins/font-awesome-4.7.0/css/font-awesome.min.css",
                "~/Content/assets/css/style.css" // nên để cuối để override
            ));

            // ✅ Bundle JS: toàn bộ script frontend
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Content/assets/js/jquery-migrate-3.0.1.min.js",
                "~/Content/assets/js/popper.min.js",
                "~/Content/assets/js/bootstrap.min.js",
                "~/Content/assets/js/jquery.easing.1.3.js",
                "~/Content/assets/js/jquery.waypoints.min.js",
                "~/Content/assets/js/jquery.stellar.min.js",
                "~/Content/assets/js/owl.carousel.min.js",
                "~/Content/assets/js/jquery.magnific-popup.min.js",
                "~/Content/assets/js/aos.js",
                "~/Content/assets/js/jquery.animateNumber.min.js",
                "~/Content/assets/js/bootstrap-datepicker.js",
                "~/Content/assets/js/scrollax.min.js",
                "~/Content/assets/js/google-map.js",
                "~/Content/assets/js/main.js",
                "~/Content/assets/plugins/Isotope/isotope.pkgd.min.js",
                "~/Content/assets/plugins/OwlCarousel2-2.2.1/owl.carousel.js",
                "~/Content/assets/plugins/easing/easing.js",
                "~/Content/assets/plugins/jquery-ui-1.12.1.custom/jquery-ui.js",
                "~/Content/assets/js/custom.js"
            ));

            // ✅ Bundle các JS backend bổ sung (Shopping, Validate)
            bundles.Add(new ScriptBundle("~/bundles/backend-js").Include(
                "~/Scripts/jsShopping.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"
            ));

            // Bật bundling/minify kể cả ở chế độ debug
            BundleTable.EnableOptimizations = true;
        }
    }
}
