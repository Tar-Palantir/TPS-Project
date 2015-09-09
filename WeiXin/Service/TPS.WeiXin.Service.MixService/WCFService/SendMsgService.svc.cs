using System;
using System.Collections.Generic;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.WCFService
{
    public class SendMsgService : ISendMsgContracts
    {
        /// <summary>
        /// 模板消息发送（只支持非企业号）
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="templateMsgParams">发送参数</param>
        /// <param name="parameters">模板参数</param>
        /// <returns>操作结果</returns>
        public OperateStatus TemplateMsg(Guid accountID, TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters)
        {
            SendMsgServiceModel model=new SendMsgServiceModel();
            return model.TemplateMsg(accountID, templateMsgParams, parameters);
        }

        /// <summary>
        /// 发送内容消息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="target">发送目标信息</param>
        /// <param name="message">消息内容</param>
        /// <returns>操作结果</returns>
        public OperateStatus SendTextMessage(Guid accountID, SendMessageTarget target, string message)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            return model.SendTextMessage(accountID, target, message);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="target">发送目标信息</param>
        /// <param name="message">消息内容项</param>
        /// <returns>操作结果</returns>
        public OperateStatus SendArticleMessage(Guid accountID, SendMessageTarget target, IList<ArticleSendItem> message)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            return model.SendArticleMessage(accountID, target, message);
        }

        /// <summary>
        /// 发送通知消息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="target">发送目标信息</param>
        /// <param name="message">消息内容项</param>
        /// <returns>操作结果</returns>
        public OperateStatus SendNewsMesaage(Guid accountID, SendMessageTarget target, IList<NewsSendItem> message)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            return model.SendNewsMesaage(accountID, target, message);
        }
    }
}
