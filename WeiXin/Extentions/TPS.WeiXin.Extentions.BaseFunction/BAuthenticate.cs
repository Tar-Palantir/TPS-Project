using Newtonsoft.Json;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.IFunction.Normal.Authenticate;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseFunction
{
    public class BAuthenticate : IOAuth
    {
        private const string GetAuthUrlFormat =
            "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state={3}#wechat_redirect";
        private const string GetAccessTokenUrlFormat =
            "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        private const string GetUserInfoUrlFOrmat =
            "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";


        public string GetAuthUrl(Account currentAccount, string redirectUrl, EnumGetAuthType getAuthType)
        {
            var scope = getAuthType == EnumGetAuthType.Base ? "snsapi_base" : "snsapi_userinfo";

            return string.Format(GetAuthUrlFormat, currentAccount.AppID, redirectUrl, scope, "");
        }

        public string GetOpenIDByCode(Account currentAccount, string code)
        {
            var accessTokenInfo = GetAccessTokenInfo(currentAccount, code);
            if (accessTokenInfo == null)
            {
                return string.Empty;
            }
            return accessTokenInfo.OpenID;
        }

        public UserInfo GetUserInfoByCode(Account currentAccount, string code)
        {
            var accessTokenInfo = GetAccessTokenInfo(currentAccount, code);
            if (accessTokenInfo == null)
            {
                return null;
            }

            var url = string.Format(GetUserInfoUrlFOrmat, accessTokenInfo.AccessToken, accessTokenInfo.OpenID);
            var responseResult = HttpHelper.GetResponseResultByGet(url);
            if (responseResult.Status != ResponseStatus.Success)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UserInfo>(responseResult.ResponseString);
        }

        private AccessTokenInfo GetAccessTokenInfo(Account currentAccount, string code)
        {
            var url = string.Format(GetAccessTokenUrlFormat, currentAccount.AppID, currentAccount.AppSecret, code);
            var responseResult = HttpHelper.GetResponseResultByGet(url);
            if (responseResult.Status != ResponseStatus.Success)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<AccessTokenInfo>(responseResult.ResponseString);
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class AccessTokenInfo
        {
            [JsonProperty(PropertyName = "access_token")]
            public string AccessToken { get; set; }

            [JsonProperty(PropertyName = "expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty(PropertyName = "refresh_token")]
            public string RefreshToken { get; set; }

            [JsonProperty(PropertyName = "openid")]
            public string OpenID { get; set; }

            [JsonProperty(PropertyName = "scope")]
            public string Scope { get; set; }

            [JsonProperty(PropertyName = "unionid")]
            public string UnionID { get; set; }
        }
    }
}
