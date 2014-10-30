using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Happy.Utility
{
    public static class HtmlExtension
    {
        public static MvcHtmlString Pager(this HtmlHelper html, int totalCount, int rowCount)
        {
            return MvcHtmlString.Create(WebUtill.Pager(totalCount, rowCount));
        }
    }
}
