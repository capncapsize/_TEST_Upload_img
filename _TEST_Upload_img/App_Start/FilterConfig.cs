using System.Web;
using System.Web.Mvc;

namespace _TEST_Upload_img
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
