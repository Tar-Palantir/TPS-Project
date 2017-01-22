using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseCorpFunction.Common
{
    public static class AccessTokenHelper
    {
        private static readonly Dictionary<Guid, AccessToken> DicAccessToken = new Dictionary<Guid, AccessToken>();
        private const string GetTokenUrlFormat_Corp = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";

        /// <summary>
        /// 获取通行令
        /// </summary>
        /// <param name="currentAccount">当前账号信息</param>
        /// <returns>通行令</returns>
        public static string GetAccessToken(Account currentAccount)
        {
            Monitor.Enter(DicAccessToken);
            try
            {
                AccessToken accessToken;
                if (DicAccessToken.ContainsKey(currentAccount.ID))
                {
                    accessToken = DicAccessToken[currentAccount.ID];
                    if (accessToken.ExpireTime > DateTime.Now)
                    {
                        return accessToken.Value;
                    }
                    DicAccessToken.Remove(currentAccount.ID);
                }

                var result =
                    HttpHelper.GetResponseResultByGet(string.Format(GetTokenUrlFormat_Corp, currentAccount.AppID,
                        currentAccount.AppSecret));
                if (result.Status != ResponseStatus.Success)
                {
                    return "";
                }
                JObject jObject = JsonConvert.DeserializeObject<JObject>(result.ResponseString);
                JToken value, time;
                if (!jObject.TryGetValue("access_token", out value) || !jObject.TryGetValue("expires_in", out time))
                {
                    return "";
                }

                accessToken = new AccessToken
                {
                    Value = value.ToString(),
                    ExpireTime = DateTime.Now.AddSeconds(time.ToObject<double>())
                };
                DicAccessToken.Add(currentAccount.ID, accessToken);

                return accessToken.Value;
            }
            finally
            {
                Monitor.Exit(DicAccessToken);
            }
        }

        /// <summary>
        /// 删除通行令
        /// </summary>
        /// <param name="accountId">账号id</param>
        /// <returns>是否删除成功</returns>
        public static bool DeleteAccessToken(Guid accountId)
        {
            if (DicAccessToken.ContainsKey(accountId))
            {
                return DicAccessToken.Remove(accountId);
            }
            return true;
        }

        class AccessToken
        {
            public string Value { set; get; }

            public DateTime ExpireTime { set; get; }
        }
    }


}
