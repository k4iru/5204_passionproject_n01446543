using System.Web;
using System.Web.Mvc;

namespace _5204_passion_project_n01446543
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
