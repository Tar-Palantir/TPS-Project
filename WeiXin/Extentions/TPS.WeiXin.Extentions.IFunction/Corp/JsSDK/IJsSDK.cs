using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Corp.JsSDK
{
    /// <summary>
    /// JsSDK接口
    /// </summary>
    public interface IJsSDK
    {
        /// <summary>
        /// 获取JS请求所需数据
        /// </summary>
        /// <param name="currentAccount">账号</param>
        /// <param name="currentUrl">当前页面的Url</param>
        /// <returns>操作结果，WeiXinJsData</returns>
        OperateStatus GetJsData(Account currentAccount, string currentUrl);
    }
}