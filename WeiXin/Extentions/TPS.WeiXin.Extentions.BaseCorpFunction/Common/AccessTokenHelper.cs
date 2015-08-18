﻿using System;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Implement;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseCorpFunction.Common
{
    public static class AccessTokenHelper
    {
        private static readonly Dictionary<Guid, AccessToken> DicAccessToken = new Dictionary<Guid, AccessToken>();
        private const string GetTokenUrlFormat_Corp = "https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}";

        public static string GetAccessToken(Guid accountID)
        {
            AccountRepository repository = new AccountRepository();
            Account currentAccount = repository.GetAccountByID(accountID);
            if (currentAccount == null)
            {
                return "";
            }

            return GetAccessToken(currentAccount);
        }

        /// <summary>
        /// 获取通行令
        /// </summary>
        /// <param name="currentAccount">当前账号信息</param>
        /// <returns>通行令</returns>
        public static string GetAccessToken(Account currentAccount)
        {
            Monitor.Enter(DicAccessToken);
            AccessToken accessToken;
            if (DicAccessToken.ContainsKey(currentAccount.ID))
            {
                accessToken = DicAccessToken[currentAccount.ID];
                if (accessToken.ExpireTime > DateTime.Now)
                {
                    return accessToken.Value;
                }
            }

            var result = HttpHelper.GetResponseResultByGet(string.Format(GetTokenUrlFormat_Corp, currentAccount.AppID, currentAccount.AppSecret));
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

            accessToken = new AccessToken { Value = value.ToString(), ExpireTime = DateTime.Now.AddSeconds(time.ToObject<double>()) };
            DicAccessToken.Add(currentAccount.ID, accessToken);
            Monitor.Exit(DicAccessToken);

            return accessToken.Value;
        }

        class AccessToken
        {
            public string Value { set; get; }

            public DateTime ExpireTime { set; get; }
        }
    }


}