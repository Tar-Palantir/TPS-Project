using TPS.WeiXin.Common.Model;
using TPS.WeiXin.Common.Model.Enums;
using TPS.WeiXin.DataAccess.Entities;

namespace TPS.WeiXin.Extentions.IFunction.Normal.Authenticate
{
    public interface IOAuth
    {
        string GetAuthUrl(Account currentAccount, string redirectUrl, EnumGetAuthType getAuthType);

        string GetOpenIDByCode(Account currentAccount, string code);

        UserInfo GetUserInfoByCode(Account currentAccount, string code);
    }
}