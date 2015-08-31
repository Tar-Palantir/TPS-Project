using System;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.WCFService
{
    public class AuthenticateService : IAuthenticateContracts
    {
        public OperateStatus GetAuthUrl(Guid accountID, string redirectUrl, EnumGetAuthType getAuthType)
        {
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            return model.GetAuthUrl(accountID, redirectUrl, getAuthType);
        }

        public OperateStatus GetOpenIDByCode(Guid accountID, string code)
        {
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            return model.GetOpenIDByCode(accountID, code);
        }

        public OperateStatus GetUserInfoByCode(Guid accountID, string code)
        {
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            return model.GetUserInfoByCode(accountID, code);
        }
    }
}
