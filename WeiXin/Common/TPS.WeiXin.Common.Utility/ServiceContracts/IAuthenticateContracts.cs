using System;
using System.ServiceModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    /// <summary>
    /// 认证相关服务契约
    /// </summary>
    [ServiceContract]
    public interface IAuthenticateContracts
    {
        /// <summary>
        /// 获取认证用Url
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="redirectUrl">重定向Url</param>
        /// <param name="getAuthType">获取认证的类型</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus GetAuthUrl(Guid accountID, string redirectUrl, EnumGetAuthType getAuthType);

        /// <summary>
        /// 使用Code获取OpenID
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="code">Code</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus GetOpenIDByCode(Guid accountID, string code);

        /// <summary>
        /// 通过Code获取用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="code">Code</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus GetUserInfoByCode(Guid accountID, string code);
    }
}
