using System.Configuration;
using Zeus.Common.Service.Client;

namespace TPS.WeiXin.Common.Utility.Client
{
    /// <summary>
    /// 发送信息服务客户端
    /// </summary>
    public static class WeiXinClient
    {
        private static string _wxServiceRootUrl = ConfigurationManager.AppSettings["WXServiceRootUrl"] ?? "";

        /// <summary>
        /// 设置服务的Url
        /// </summary>
        /// <param name="serviceUrl">服务Url</param>
        public static void SetWXServiceRootUrl(string serviceUrl)
        {
            _wxServiceRootUrl = serviceUrl;
        }

        public static T GetClientProxy<T>()
        {
            WCFClient client = new WCFClient();
            return client.GetClientProxy<T>(_wxServiceRootUrl);
        }
    }
}
