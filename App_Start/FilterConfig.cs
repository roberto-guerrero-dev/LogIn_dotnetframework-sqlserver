using System.Web;
using System.Web.Mvc;

namespace LogIn_dotnetframework_sqlserver
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
