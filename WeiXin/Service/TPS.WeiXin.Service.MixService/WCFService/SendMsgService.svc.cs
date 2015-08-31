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
        public OperateStatus TemplateMsg(Guid accountID, TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters)
        {
            SendMsgServiceModel model=new SendMsgServiceModel();
            return model.TemplateMsg(accountID, templateMsgParams, parameters);
        }
        
        public OperateStatus SendTextMessage(Guid accountID, SendMessageTarget target, string message)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            return model.SendTextMessage(accountID, target, message);
        }

        public OperateStatus SendArticleMessage(Guid accountID, SendMessageTarget target, IList<ArticleSendItem> message)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            return model.SendArticleMessage(accountID, target, message);
        }

        public OperateStatus SendNewsMesaage(Guid accountID, SendMessageTarget target, IList<NewsSendItem> message)
        {
            SendMsgServiceModel model = new SendMsgServiceModel();
            return model.SendNewsMesaage(accountID, target, message);
        }
    }
}
