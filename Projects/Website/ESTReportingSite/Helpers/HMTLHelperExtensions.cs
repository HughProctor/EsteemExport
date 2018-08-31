using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ESTReportingSite
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this HtmlHelper html, string controller = null, string action = null)
        {
            string cssClass = "active";
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                cssClass : String.Empty;
        }

        public static string PageClass(this HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static string SpaceCapital(this HtmlHelper html, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if (char.IsUpper(text[i - 1]))
                        if (i < text.Length - 1 && !char.IsUpper(text[i + 1])) //preserveAcronyms && 
                            newText.Append(' ');
                        else;
                    else if (text[i - 1] != ' ')
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        public static string SpaceCapital(this HtmlHelper html, MvcHtmlString inputText)
        {
            var text = inputText.ToString();
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if (char.IsUpper(text[i - 1]))
                        if (i < text.Length - 1 && !char.IsUpper(text[i + 1])) //preserveAcronyms && 
                            newText.Append(' ');
                        else;
                    else if (text[i - 1] != ' ')
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
