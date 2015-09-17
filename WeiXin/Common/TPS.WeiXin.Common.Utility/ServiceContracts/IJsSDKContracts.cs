using System;
using System.ServiceModel;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    /// <summary>
    /// JsSDK契约
    /// </summary>
    [ServiceContract]
    public interface IJsSDKContracts
    {
        /// <summary>
        /// 获取JS请求所需数据
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="currentUrl">当前页面的Url</param>
        /// <returns>操作结果，WeiXinJsData</returns>
        [OperationContract]
        OperateStatus GetJsData(Guid accountID, string currentUrl);
    }
}