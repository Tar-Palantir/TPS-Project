using System.Web.Mvc;
using TPS.WeiXin.Common.Helper;

namespace TPS.WeiXin.Service.MixService.Controllers
{
    public class HiddenController : Controller
    {
        public ActionResult IocRefresh()
        {
            FunctionFactory.RefreshContainer();
            return Content("Good");
        }
    }
}