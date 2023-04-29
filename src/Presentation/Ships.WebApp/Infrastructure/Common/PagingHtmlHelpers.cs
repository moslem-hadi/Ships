using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ships.Application.Common.Models;
using System.Text;

namespace Ships.WebApp.Infrastructure.Common;

public static class PagingHtmlHelpers
{
    public static IHtmlContent PageLinks
    (this IHtmlHelper htmlHelper, PageInfo pageInfo, Func<int, string> PageUrl)
    {
        StringBuilder pagingTags = new StringBuilder();
        //Prev Page
        //if (pageInfo.PageNumber > 1)
        {
            pagingTags.Append(GetTagString
                             ("Prev", PageUrl(pageInfo.PageNumber - 1),
                             pageInfo.PageNumber==1 , false));
        }
        //Page Numbers
        for (int i = 1; i <= pageInfo.LastPage; i++)
        {
            pagingTags.Append(GetTagString(i.ToString(), PageUrl(i), false, i == pageInfo.PageNumber));
        }
        //Next Page
       // if (pageInfo.PageNumber < pageInfo.LastPage)
        {
            pagingTags.Append(GetTagString
                             ("Next", PageUrl(pageInfo.PageNumber + 1),
                             pageInfo.PageNumber == pageInfo.LastPage,
                             false
                             ));
        }
        //paging tags
        return new HtmlString(pagingTags.ToString());
    }

    private static string GetTagString(string innerHtml, string hrefValue, bool disabled, bool active)
    {
        TagBuilder li = new TagBuilder("li");
        li.MergeAttribute("class", $"paginate_button page-item {(disabled ? "disabled" : "")} {(active ? "active" : "")}");

        TagBuilder tag = new TagBuilder("a"); 
        tag.MergeAttribute("class", "page-link");
        tag.MergeAttribute("href", hrefValue);
        tag.InnerHtml.Append(" " + innerHtml + "  ");

        li.InnerHtml.AppendHtml(tag);
        using (var sw = new System.IO.StringWriter())
        {
            li.WriteTo(sw, System.Text.Encodings.Web.HtmlEncoder.Default);
            return sw.ToString();
        }
    }
}
