using System.Web;
using System.Web.Mvc;

namespace Happy.Hims.Pad
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}