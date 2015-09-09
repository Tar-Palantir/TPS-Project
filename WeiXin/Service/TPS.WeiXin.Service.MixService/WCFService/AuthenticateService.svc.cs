using System;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.WCFService
{
    public class AuthenticateService : IAuthenticateContracts
    {
        /// <summary>
        /// 获取认证用Url
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="redirectUrl">重定向Url</param>
        /// <param name="getAuthType">获取认证的类型</param>
        /// <returns>操作结果</returns>
        public OperateStatus GetAuthUrl(Guid accountID, string redirectUrl, EnumGetAuthType getAuthType)
        {
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            return model.GetAuthUrl(accountID, redirectUrl, getAuthType);
        }

        /// <summary>
        /// 使用Code获取OpenID
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="code">Code</param>
        /// <returns>操作结果</returns>
        public OperateStatus GetOpenIDByCode(Guid accountID, string code)
        {
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            return model.GetOpenIDByCode(accountID, code);
        }

        /// <summary>
        /// 通过Code获取用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="code">Code</param>
        /// <returns>操作结果</returns>
        public OperateStatus GetUserInfoByCode(Guid accountID, string code)
        {
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            return model.GetUserInfoByCode(accountID, code);
        }
    }
}
