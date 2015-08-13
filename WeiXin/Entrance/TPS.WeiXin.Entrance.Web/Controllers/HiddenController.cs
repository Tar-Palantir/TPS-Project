using System.Web.Mvc;
using TPS.WeiXin.Extentions.BaseFunction.Common;

namespace TPS.WeiXin.Entrance.Web.Controllers
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