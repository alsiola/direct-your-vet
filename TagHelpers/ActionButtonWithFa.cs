using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DYV.TagHelpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent ActionButtonWithFa(this IHtmlHelper html, string buttonText, string action, string controller, string buttonClasses, string faClasses )
        {
            return html.ActionButtonWithFa(buttonText, action, controller, null, buttonClasses, faClasses);
        }

        public static IHtmlContent ActionButtonWithFa(this IHtmlHelper html, string buttonText, string action, string controller, object urlParams, string buttonClasses, string faClasses)
        {
            var url = new UrlHelper(html.ViewContext);

            var faBuilder = new TagBuilder("i");

            faBuilder.MergeAttribute("class", faClasses);

            var anchorBuilder = new TagBuilder("a");

            anchorBuilder.MergeAttribute("class", buttonClasses);
            anchorBuilder.MergeAttribute("href", url.Action(action, controller, urlParams));
            anchorBuilder.InnerHtml.AppendHtml(faBuilder);
            anchorBuilder.InnerHtml.Append(buttonText);

            return anchorBuilder;
        }
    }
}
