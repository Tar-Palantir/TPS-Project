using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.BaseFunction.Common;
using TPS.WeiXin.Extentions.IFunction.Normal.JsSDK;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper;
using Zeus.Common.Helper.Cryptography;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseFunction
{
    /// <summary>
    /// JsSDK
    /// </summary>
    public class BJsSDK : IJsSDK
    {
        private static readonly Dictionary<Guid, Ticket> DicTicket = new Dictionary<Guid, Ticket>();
        private const string GetTicketUrlFormat =
            "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";

        /// <summary>
        /// 获取JS请求所需数据
        /// </summary>
        /// <param name="currentAccount">账号</param>
        /// <param name="currentUrl">当前页面的Url</param>
        /// <returns>操作结果，WeiXinJsData</returns>
        public OperateStatus GetJsData(Account currentAccount, string currentUrl)
        {
            string noncestr = DateTime.Now.ToString("yyyyMMddHHmmss");
            string timestamp = DateTime.Now.ToTimestamp().ToString();
            string signature = GetSignature(currentAccount, noncestr, timestamp, currentUrl);

            var returnValue = JsonConvert.SerializeObject(new WeiXinJsData
            {
                AppID = currentAccount.AppID,
                NonceStr = noncestr,
                Timestamp = timestamp,
                Signature = signature
            });
            return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = returnValue };
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="currentAccount">当前账号信息</param>
        /// <param name="noncestr">随机字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="currentUrl">当前页面的Url</param>
        /// <returns>签名</returns>
        private static string GetSignature(Account currentAccount, string noncestr, string timestamp, string currentUrl)
        {
            var dicPreParams = new Dictionary<string, string>
            {
                {"noncestr",noncestr},
                {"timestamp",timestamp},
                {"jsapi_ticket",GetTicket(currentAccount)},
                {"url",currentUrl}
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
            Monitor.Enter(DicTicket);
            try
            {
                Ticket ticket;
                if (DicTicket.ContainsKey(currentAccount.ID))
                {
                    ticket = DicTicket[currentAccount.ID];
                    if (ticket.ExpireTime > DateTime.Now)
                    {
                        return ticket.Value;
                    }
                    DicTicket.Remove(currentAccount.ID);
                }

                var accessToken = AccessTokenHelper.GetAccessToken(currentAccount);
                var result = HttpHelper.GetResponseResultByGet(string.Format(GetTicketUrlFormat, accessToken),
                    timeout: 1000);
                if (result.Status != ResponseStatus.Success)
                {
                    return "";
                }
                JObject jObject = JsonConvert.DeserializeObject<JObject>(result.ResponseString);
                JToken value;
                if (!jObject.TryGetValue("ticket", out value))
                {
                    return "";
                }

                ticket = new Ticket { Value = value.ToString(), ExpireTime = DateTime.Now.AddSeconds(7190) };
                DicTicket.Add(currentAccount.ID, ticket);

                return ticket.Value;
            }
            finally
            {
                Monitor.Exit(DicTicket);
            }

        }

        /// <summary>
        /// 票据数据
        /// </summary>
        class Ticket
        {
            /// <summary>
            /// 票据值
            /// </summary>
            public string Value { get; set; }

            /// <summary>
            /// 过期时间
            /// </summary>
            public DateTime ExpireTime { get; set; }
        }
    }
}