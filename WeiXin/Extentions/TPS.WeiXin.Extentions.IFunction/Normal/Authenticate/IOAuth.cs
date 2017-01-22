using System;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.DataAccess.Entities;

namespace TPS.WeiXin.Extentions.IFunction.Normal.Authenticate
{
    public interface IOAuth
    {
        string GetAuthUrl(Account currentAccount, string redirectUrl, EnumGetAuthType getAuthType);

        string GetOpenIDByCode(Account currentAccount, string code);

        UserInfo GetUserInfoByCode(Account currentAccount, string code);
        
        bool DeleteAccessToken(Guid accountId);
    }
}