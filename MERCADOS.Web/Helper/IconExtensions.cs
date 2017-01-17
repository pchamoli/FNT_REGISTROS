﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

public static class IconExtensions
{
    public static MvcHtmlString IconItem(this HtmlHelper htmlHelper, string nombreIcon, string colorIcon)
    {
        var i = new TagBuilder("i");
        i.AddCssClass(nombreIcon);
        i.AddCssClass(colorIcon);
        return MvcHtmlString.Create(i.ToString());
    }

    public static MvcHtmlString TextIconItem(this HtmlHelper htmlHelper, string text, string nombreIcon, string colorIcon)
    {
        return MvcHtmlString.Create(IconItem(htmlHelper, nombreIcon, colorIcon).ToString() + " " + text);
    }

    public static MvcHtmlString IconlinkItem(this HtmlHelper htmlHelper, string title, string classAction, string idElemento, string nombreIcon, string colorIcon, bool visualizar)
    {
        if (!visualizar)
            return MvcHtmlString.Create("");

        var a = new TagBuilder("a");
        a.InnerHtml = IconItem(htmlHelper, nombreIcon, colorIcon).ToHtmlString();
        a.Attributes.Add("href","#");
        a.Attributes.Add("title", title);
        a.Attributes.Add("id", idElemento);
        a.AddCssClass(classAction);

        return MvcHtmlString.Create(a.ToString());
    }

    public static MvcHtmlString TextIconlinkItem(this HtmlHelper htmlHelper, string title, string classAction, string idElemento, string text, string nombreIcon, string colorIcon, bool visualizar)
    {
        if (!visualizar)
            return MvcHtmlString.Create("");

        var a = new TagBuilder("a");
        a.InnerHtml = TextIconItem(htmlHelper, text, nombreIcon, colorIcon).ToHtmlString();
        a.Attributes.Add("href", "#");
        a.Attributes.Add("title", title);
        a.Attributes.Add("id", idElemento);
        a.AddCssClass(classAction);

        return MvcHtmlString.Create(a.ToString());
    }
}