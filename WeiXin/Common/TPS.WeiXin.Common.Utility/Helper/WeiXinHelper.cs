using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TPS.WeiXin.Common.Utility.Helper
{
    public static class WeiXinHelper
    {
        /// <summary>
        /// 当前浏览器是否是微信浏览器
        /// </summary>
        /// <returns></returns>
        public static bool IsWeiXinExplorer()
        {
            //Mozilla/5.0 (iPhone; CPU iPhone OS 5_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Mobile/9B176 MicroMessenger/4.3.2
            //Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255
            return HttpContext.Current.Request.UserAgent != null &&
                   HttpContext.Current.Request.UserAgent.Contains("MicroMessenger");
        }
    }
}
