using System;
using System.Collections.Generic;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Extentions.IFunction.Corp.SendMsg;
using TPS.WeiXin.Extentions.IFunction.Normal.SendMsg;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.Models
{
    public class SendMsgServiceModel
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
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<ITemplateMsg>();
            OperateStatus status = func.SendTemplateMsg(currentAccount, templateMsgParams, parameters);
            return status;
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
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<ISend>();
            OperateStatus status = func.SendTextMessage(currentAccount, target, message);
            return status;
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
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<ISend>();
            OperateStatus status = func.SendArticleMessage(currentAccount, target, message);
            return status;
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
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<ISend>();
            OperateStatus status = func.SendNewsMesaage(currentAccount, target, message);
            return status;
        }
    }
}