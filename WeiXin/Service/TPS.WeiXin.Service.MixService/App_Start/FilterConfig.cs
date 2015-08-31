using System.Web.Mvc;
using TPS.Common.Utility.UI;

namespace TPS.WeiXin.Service.MixService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GlobalExceptionHandleFilter());
        }
    }
}
