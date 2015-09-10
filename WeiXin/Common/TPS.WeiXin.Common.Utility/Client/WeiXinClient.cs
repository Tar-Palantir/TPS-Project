using System;
using System.Configuration;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using Zeus.Common.Service.Client;

namespace TPS.WeiXin.Common.Utility.Client
{
    /// <summary>
    /// 发送信息服务客户端
    /// </summary>
    public class WeiXinClient : IDisposable
    {
        private static readonly string _wxServiceRootUrl = ConfigurationManager.AppSettings["WXServiceRootUrl"] ?? "";
        private readonly WCFClient client = new WCFClient();

        /// <summary>
        /// 获取认证服务客户端代理
        /// </summary>
        /// <returns></returns>
        public IAuthenticateContracts GetAuthClientProxy()
        {
            return client.GetClientProxy<IAuthenticateContracts>(_wxServiceRootUrl + "/WCFService/AuthenticateService.svc");
        }

        /// <summary>
        /// 获取通讯录服务客户端代理
        /// </summary>
        /// <returns></returns>
        public IContactsContracts GetContactsClientProxy()
        {
            return client.GetClientProxy<IContactsContracts>(_wxServiceRootUrl + "/WCFService/ContactsService.svc");
        }

        /// <summary>
        /// 获取自定义菜单服务客户端代理
        /// </summary>
        /// <returns></returns>
        public ICustomMenuContracts GetCustomMenuClientProxy()
        {
            return client.GetClientProxy<ICustomMenuContracts>(_wxServiceRootUrl + "/WCFService/CustomMenuService.svc");
        }

        /// <summary>
        /// 获取发送消息服务客户端代理
        /// </summary>
        /// <returns></returns>
        public ISendMsgContracts GetSendMsgClientProxy()
        {
            return client.GetClientProxy<ISendMsgContracts>(_wxServiceRootUrl + "/WCFService/SendMsgService.svc");
        }

        /// <summary>
        /// 获取用户管理服务客户端代理
        /// </summary>
        /// <returns></returns>
        public IUserManageContracts GetUserManageClientProxy()
        {
            return client.GetClientProxy<IUserManageContracts>(_wxServiceRootUrl + "/WCFService/UserManageService.svc");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }
    }
}
