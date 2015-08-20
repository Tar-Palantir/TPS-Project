using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Implement;
using TPS.WeiXin.Extentions.BaseCorpFunction.Exts;
using Zeus.Common.Helper;
using Zeus.Common.Helper.Cryptography;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseCorpFunction.Common
{
    /// <summary>
    /// 微信帮助类
    /// </summary>
    public class JsSDKHelper
    {

        /// <summary>
        /// 获取JS请求所需数据
        /// </summary>
        /// <returns></returns>
        public static WeiXinJsData GetJsData(Guid accountID)
        {
            AccountRepository repository = new AccountRepository();
            Account currentAccount = repository.GetById(accountID);
            if (currentAccount == null)
            {
                return null;
            }

            string noncestr = DateTime.Now.ToString("yyyyMMddHHmmss");
            string timestamp = DateTime.Now.ToTimestamp().ToString();
            string signature = GetSignature(currentAccount, noncestr, timestamp);

            return new WeiXinJsData
            {
                AppID = currentAccount.AppID,
                NonceStr = noncestr,
                Timestamp = timestamp,
                Signature = signature
            };
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="currentAccount">当前账号信息</param>
        /// <param name="noncestr">随机字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns>签名</returns>
        private static string GetSignature(Account currentAccount, string noncestr, string timestamp)
        {
            var dicPreParams = new Dictionary<string, string>
            {
                {"noncestr",noncestr},
                {"timestamp",timestamp},
                {"jsapi_ticket",GetTicket(currentAccount)},
                {"url",HttpContext.Current.Request.Url.AbsoluteUri}
            };

            var preStr = HttpHelper.CreateLinkString(dicPreParams);
            return HashCryptography.Sha1Encrypt(preStr);
        }

        /// <summary>
        /// 获取票据
        /// </summary>
        /// <param name="currentAccount">当前账号信息</param>
        /// <returns>票据</returns>
        private static string GetTicket(Account currentAccount)
        {
            if (HttpContext.Current.Session["T"] != null)
            {
                var data = HttpContext.Current.Session["T"] as WeiXinData;
                if (data.ExpiredTime > DateTime.Now)
                {
                    return data.Ticket;
                }
            }

            var accessToken = AccessTokenHelper.GetAccessToken(currentAccount);
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}",
                accessToken);

            var responseResult = HttpHelper.GetResponseResultByGet(url);
            if (responseResult.Status != ResponseStatus.Success)
            {
                HttpContext.Current.Response.Write(responseResult.ExceptionMessages);
                HttpContext.Current.Response.End();
                return "";
            }
            //{"access_token":"ACCESS_TOKEN","expires_in":7200}
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseResult.ResponseString);
            if (result.ContainsKey("ticket"))
            {
                var ticket = result["ticket"];
                HttpContext.Current.Session["T"] = new WeiXinData { Ticket = ticket, ExpiredTime = DateTime.Now.AddSeconds(7190) };
                return ticket;
            }

            HttpContext.Current.Response.Write(responseResult.ResponseString);
            HttpContext.Current.Response.End();
            return "";
        }

        /// <summary>
        /// 微信数据
        /// </summary>
        class WeiXinData
        {
            /// <summary>
            /// 票据
            /// </summary>
            public string Ticket { get; set; }

            /// <summary>
            /// 过期时间
            /// </summary>
            public DateTime ExpiredTime { get; set; }
        }
    }
}