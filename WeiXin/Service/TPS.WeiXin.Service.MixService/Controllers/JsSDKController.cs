using System;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Service.MixService.Controllers
{
    public class JsSDKController : BaseServiceController
    {
        /// <summary>
        /// 日志记录中的登录名
        /// </summary>
        protected override string LogonName
        {
            get { return ""; }
        }

        public ServiceResult GetJsData(Guid accountID, string currentUrl)
        {
            JsSDKServiceModel model = new JsSDKServiceModel();
            var status = model.GetJsData(accountID, currentUrl);
            return new ServiceResult(status);
        }
    }
}