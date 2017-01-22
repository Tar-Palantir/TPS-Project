using System;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;

namespace TPS.WeiXin.Extentions.IFunction.Corp.Authenticate
{
    public interface IOAuth
    {
        UserInfo GetUserInfoByCode(Account currentAccount, string code);
        
        bool DeleteAccessToken(Guid accountId);
    }
}