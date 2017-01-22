using System;
using Newtonsoft.Json;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.BaseCorpFunction.Common;
using TPS.WeiXin.Extentions.IFunction.Corp.Authenticate;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseCorpFunction
{
    public class CAuthenticate : IOAuth
    {
        private const string GetUserInfoUrlFormat = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}";
        
        public UserInfo GetUserInfoByCode(Account currentAccount, string code)
        {
            var accessToken = AccessTokenHelper.GetAccessToken(currentAccount);

            var url = string.Format(GetUserInfoUrlFormat, accessToken, code);
            var responseResult = HttpHelper.GetResponseResultByGet(url);
            if (responseResult.Status != ResponseStatus.Success)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UserInfo>(responseResult.ResponseString);
        }

        public bool DeleteAccessToken(Guid accountId)
        {
            return AccessTokenHelper.DeleteAccessToken(accountId);
        }

    }
}
