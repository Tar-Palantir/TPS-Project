using System;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
using TeaLife.Common.Utility.Helper;
using Zeus.Common.Helper;
using Zeus.Common.Helper.Cryptography;
using Zeus.Common.Helper.Web;

namespace TeaLife.UI.MobileSite.Common
{
    /// <summary>
    /// 微信帮助类
    /// </summary>
    public class WeiXinHelper
    {
        /// <summary>
        /// AppID
        /// </summary>
        private static readonly string AppID = CommonHelper.GetStaticConfig("WXAppID");
        /// <summary>
        /// AppKey
        /// </summary>
        private static readonly string AppKey = CommonHelper.GetStaticConfig("WXJsAPPKey");

        /// <summary>
        /// 当前浏览器是否是微信浏览器
        /// </summary>
        /// <returns></returns>
        public static bool IsWeiXinExplorer()
        {
            //Mozilla/5.0 (iPhone; CPU iPhone OS 5_1 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Mobile/9B176 MicroMessenger/4.3.2
            //Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255
            return HttpContext.Current.Request.UserAgent != null &&
                   HttpContext.Current.Request.UserAgent.Contains("MicroMessenger");
        }

        /// <summary>
        /// 获取JS请求所需数据
        /// </summary>
        /// <returns></returns>
        public static WeiXinJsData GetJsData()
        {
            string noncestr = DateTime.Now.ToString("yyyyMMddHHmmss");
            string timestamp = DateTime.Now.ToTimestamp().ToString();
            string signature = GetSignature(noncestr, timestamp);

            return new WeiXinJsData
            {
                AppID = AppID,
                NonceStr = noncestr,
                Timestamp = timestamp,
                Signature = signature
            };
        }

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="noncestr">随机字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <returns>签名</returns>
        private static string GetSignature(string noncestr, string timestamp)
        {
            var dicPreParams = new Dictionary<string, string>
            {
                {"noncestr",noncestr},
                {"timestamp",timestamp},
                {"jsapi_ticket",GetTicket()},
                {"url",HttpContext.Current.Request.Url.AbsoluteUri}
            };

            var preStr = HttpHelper.CreateLinkString(dicPreParams);
            return HashCryptography.Sha1Encrypt(preStr);
        }

        /// <summary>
        /// 获取票据
        /// </summary>
        /// <returns>票据</returns>
        private static string GetTicket()
        {
            if (HttpContext.Current.Session["T"] != null)
            {
                var data = HttpContext.Current.Session["T"] as WeiXinData;
                if (data.ExpiredTime > DateTime.Now)
                {
                    return data.Ticket;
                }
            }

            var accessToken = GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi",
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
        /// 获取通行令
        /// </summary>
        /// <returns>通行令</returns>
        private static string GetAccessToken()
        {
            var url = string.Format(
                "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}",
                AppID, AppKey);

            var responseResult = HttpHelper.GetResponseResultByGet(url);
            if (responseResult.Status != ResponseStatus.Success)
            {
                HttpContext.Current.Response.Write(responseResult.ExceptionMessages);
                HttpContext.Current.Response.End();
                return "";
            }
            //{"access_token":"ACCESS_TOKEN","expires_in":7200}
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseResult.ResponseString);
            if (result.ContainsKey("access_token"))
            {
                return result["access_token"];
            }

            HttpContext.Current.Response.Write(responseResult.ResponseString);
            HttpContext.Current.Response.End();
            return "";
        }
    }

    /// <summary>
    /// 微信数据
    /// </summary>
    public class WeiXinData
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

    /// <summary>
    /// 微信JS请求数据
    /// </summary>
    public class WeiXinJsData
    {
        /// <summary>
        /// AppID
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string NonceStr { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
    }
}