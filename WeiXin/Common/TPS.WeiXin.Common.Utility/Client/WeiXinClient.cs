using System.Configuration;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using Zeus.Common.Service.Client;

namespace TPS.WeiXin.Common.Utility.Client
{
    /// <summary>
    /// 发送信息服务客户端
    /// </summary>
    public static class WeiXinClient
    {
        private static readonly string _wxServiceRootUrl = ConfigurationManager.AppSettings["WXServiceRootUrl"] ?? "";


        /// <summary>
        /// 获取认证服务客户端代理
        /// </summary>
        /// <returns></returns>
        public static IAuthenticateContracts GetAuthClientProxy()
        {
            WCFClient client = new WCFClient();
            return client.GetClientProxy<IAuthenticateContracts>(_wxServiceRootUrl + "/WCFService/AuthenticateService.svc");
        }

        /// <summary>
        /// 获取通讯录服务客户端代理
        /// </summary>
        /// <returns></returns>
        public static IContactsContracts GetContactsClientProxy()
        {
            WCFClient client = new WCFClient();
            return client.GetClientProxy<IContactsContracts>(_wxServiceRootUrl + "/WCFService/ContactsService.svc");
        }

        /// <summary>
        /// 获取自定义菜单服务客户端代理
        /// </summary>
        /// <returns></returns>
        public static ICustomMenuContracts GetCustomMenuClientProxy()
        {
            WCFClient client = new WCFClient();
            return client.GetClientProxy<ICustomMenuContracts>(_wxServiceRootUrl + "/WCFService/CustomMenuService.svc");
        }

        /// <summary>
        /// 获取发送消息服务客户端代理
        /// </summary>
        /// <returns></returns>
        public static ISendMsgContracts GetSendMsgClientProxy()
        {
            WCFClient client = new WCFClient();
            return client.GetClientProxy<ISendMsgContracts>(_wxServiceRootUrl + "/WCFService/SendMsgService.svc");
        }

        /// <summary>
        /// 获取用户管理服务客户端代理
        /// </summary>
        /// <returns></returns>
        public static IUserManageContracts GetUserManageClientProxy()
        {
            WCFClient client = new WCFClient();
            return client.GetClientProxy<IUserManageContracts>(_wxServiceRootUrl + "/WCFService/UserManageService.svc");
        }

    }
}
