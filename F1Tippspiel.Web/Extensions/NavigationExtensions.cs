using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace F1Tippspiel.Web.Extensions
{
    /// <summary>
    /// HtmlHelper Extensions for easy creation of navigation links
    /// </summary>
    public static class NavigationExtensions
    {
        /// <summary>
        /// Generates an ActionLink based on given Controller and Action.<br/>
        /// If the current route matches the given Controller and Action, the
        /// generated ActionLink will be marked as active<p/>
        /// More Information [german]: http://blog.bigbasti.com/asp-net-mvc-aktuellen-menulink-hervorheben/
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">Text to display as link</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route parameters</param>
        /// <param name="htmlAttributes">HTML attributes to add</param>
        /// <param name="wrapperElement">Name of the HTML element the new link should be wrapped in - default: li</param>
        /// <param name="flag">Name of the CSS class to be added to matching "active" links - default: active</param>
        /// <returns></returns>
        public static MvcHtmlString NavigationActionLink(this HtmlHelper htmlHelper, string linkText,
                                                         string actionName, string controllerName, object routeValues = null,
                                                         object htmlAttributes = null,
                                                         string wrapperElement = "li", string flag = "active")
        {
            var generatedLink = HtmlHelper.GenerateLink(
                            htmlHelper.ViewContext.RequestContext,
                            htmlHelper.RouteCollection, linkText,
                            null, actionName, controllerName,
                            new RouteValueDictionary(routeValues),
                            HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            var wrappedElement = WrapGeneratedLink(
                            htmlHelper, actionName,
                            controllerName, wrapperElement, flag, generatedLink);

            return MvcHtmlString.Create(wrappedElement.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Wraps the generated link into given HTML tag<br/>
        /// If the current url matches the controller and action
        /// the new link will get a 'active' CSS mark
        /// </summary>
        /// <param name="htmlHelper">HtmlHelper instance</param>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="wrapper">Wrapper HTML element (Example: li or p)"></param>
        /// <param name="flag">Name of the CSS class to be added if the current route matches the controller and action</param>
        /// <param name="generatedLink">the link to be wrapped</param>
        /// <returns>HTML containing the wrapped link</returns>
        private static TagBuilder WrapGeneratedLink(HtmlHelper htmlHelper, string actionName, string controllerName,
                                                    string wrapper, string flag, string generatedLink)
        {
            var wrapperElement = new TagBuilder(wrapper)
            {
                InnerHtml = generatedLink
            };

            if (CurrentRouteMatchesGeneratedUrl(actionName, controllerName, htmlHelper))
            {
                wrapperElement.AddCssClass(flag);
            }
            return wrapperElement;
        }

        /// <summary>
        /// Checks if the current route matches the given action and controller
        /// </summary>
        /// <param name="actionName">Actuion name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="htmlHelper">HtmlHelper instance</param>
        /// <returns>True if the given controller and action are part of the current route</returns>
        private static bool CurrentRouteMatchesGeneratedUrl(string actionName, string controllerName, HtmlHelper htmlHelper)
        {
            var currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            var currentController = (string)htmlHelper.ViewContext.RouteData.Values["controller"];

            return string.Equals(currentController, controllerName,
                                StringComparison.CurrentCultureIgnoreCase) &&
                  string.Equals(currentAction, actionName,
                                StringComparison.CurrentCultureIgnoreCase);
        }
    }
}