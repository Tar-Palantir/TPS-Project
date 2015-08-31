using System;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Service.MixService.Controllers
{
    public class AuthenticateController : BaseServiceController
    {
        /// <summary>
        /// 日志记录中的登录名
        /// </summary>
        protected override string LogonName
        {
            get { return ""; }
        }

        public ServiceResult GetAuthUrl(Guid accountID, string redirectUrl, EnumGetAuthType getAuthType)
        {
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            var status = model.GetAuthUrl(accountID, redirectUrl, getAuthType);
            return new ServiceResult(status);
        }

        public ServiceResult GetOpenIDByCode(Guid accountID, string code)
        {
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            var status = model.GetOpenIDByCode(accountID, code);
            return new ServiceResult(status);
        }

        public ServiceResult GetUserInfoByCode(Guid accountID, string code)
        {
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            var status = model.GetUserInfoByCode(accountID, code);
            return new ServiceResult(status);
        }
    }
}