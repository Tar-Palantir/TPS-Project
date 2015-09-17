using System;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Extentions.IFunction.Corp.JsSDK;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.Models
{
    public class JsSDKServiceModel
    {
        /// <summary>
        /// 获取JS请求所需数据
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="currentUrl">当前页面的Url</param>
        /// <returns>操作结果，WeiXinJsData</returns>
        public OperateStatus GetJsData(Guid accountID, string currentUrl)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }
            OperateStatus status;
            if (account.IsCorp)
            {
                var func = FunctionFactory.GetFunctionInstance<IJsSDK>();
                status = func.GetJsData(account, currentUrl);
            }
            else
            {
                var func = FunctionFactory.GetFunctionInstance<Extentions.IFunction.Normal.JsSDK.IJsSDK>();
                status = func.GetJsData(account, currentUrl);
            }

            return status;
        }
    }
}