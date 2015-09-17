using System;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.WCFService
{
    public class JsSDK : IJsSDKContracts
    {
        /// <summary>
        /// 获取JS请求所需数据
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="currentUrl">当前页面的Url</param>
        /// <returns>操作结果，WeiXinJsData</returns>
        public OperateStatus GetJsData(Guid accountID, string currentUrl)
        {
            JsSDKServiceModel model = new JsSDKServiceModel();
            return model.GetJsData(accountID, currentUrl);
        }
    }
}
