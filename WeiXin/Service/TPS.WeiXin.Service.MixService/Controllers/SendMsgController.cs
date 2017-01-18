using System;
using System.Collections.Generic;
using System.Web.ModelBinding;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Service.MixService.Controllers
{
    public class SendMsgController : BaseServiceController
    {
        /// <summary>
        /// 日志记录中的登录名
        /// </summary>
        protected override string LogonName
        {
            get { return ""; }
        }

        public ServiceResult TemplateMsg(Guid accountID, TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            var status = model.TemplateMsg(accountID, templateMsgParams, parameters);
            return new ServiceResult(status);
        }


        public ServiceResult SendTextMessage(Guid accountID, SendMessageTarget target, string message)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            var status = model.SendTextMessage(accountID, target, message);
            return new ServiceResult(status);
        }

        public ServiceResult SendArticleMessage(Guid accountID, SendMessageTarget target, IList<ArticleSendItem> message)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            var status = model.SendArticleMessage(accountID, target, message);
            return new ServiceResult(status);
        }

        public ServiceResult SendNewsMesaage(Guid accountID, SendMessageTarget target, IList<NewsSendItem> message)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            var status = model.SendNewsMesaage(accountID, target, message);
            return new ServiceResult(status);
        }
    }
}