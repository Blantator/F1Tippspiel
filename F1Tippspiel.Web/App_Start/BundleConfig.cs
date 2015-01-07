using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace F1Tippspiel.Web.App_Start
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Verwenden Sie die Entwicklungsversion von Modernizr zum Entwickeln und Erweitern Ihrer Kenntnisse. Wenn Sie dann
            // für die Produktion bereit sind, verwenden Sie das Buildtool unter "http://modernizr.com", um nur die benötigten Tests auszuwählen.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));


            bundles.Add(new StyleBundle("~/Content/font-awesome").Include("~/Content/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular/angular.*",
                        "~/Scripts/angular/angular-route*",
                        "~/Scripts/angular/angular-loader*",
                        "~/Scripts/angular/angular-resource*",
                        "~/Scripts/angular/angular-local-storage*",
                        "~/Scripts/angular/i18n/angular-locale_de.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory(
                        "~/Scripts/app", "*.js", true));

            // Festlegen von "EnableOptimizations" auf "false" für Debugzwecke. Weitere Informationen
            // finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862".
            BundleTable.EnableOptimizations = true;
        }
    }
}