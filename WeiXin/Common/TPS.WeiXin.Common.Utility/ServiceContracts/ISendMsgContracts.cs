using System;
using System.Collections.Generic;
using System.ServiceModel;
using TPS.WeiXin.Common.SrvcModel;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    /// <summary>
    /// 发送消息服务契约
    /// </summary>
    [ServiceContract]
    public interface ISendMsgContracts
    {
        /// <summary>
        /// 模板消息发送（只支持非企业号）
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="templateMsgParams">发送参数</param>
        /// <param name="parameters">模板参数</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus TemplateMsg(Guid accountID, TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters);

        /// <summary>
        /// 发送内容消息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="target">发送目标信息</param>
        /// <param name="message">消息内容</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus SendTextMessage(Guid accountID, SendMessageTarget target, string message);

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="target">发送目标信息</param>
        /// <param name="message">消息内容项</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus SendArticleMessage(Guid accountID, SendMessageTarget target, IList<ArticleSendItem> message);

        /// <summary>
        /// 发送通知消息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="target">发送目标信息</param>
        /// <param name="message">消息内容项</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus SendNewsMesaage(Guid accountID, SendMessageTarget target, IList<NewsSendItem> message);
    }
}
