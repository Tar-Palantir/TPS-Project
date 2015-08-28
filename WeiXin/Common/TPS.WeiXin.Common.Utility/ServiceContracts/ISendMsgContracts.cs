using System;
using System.Collections.Generic;
using TPS.WeiXin.Common.Model;
using TPS.WeiXin.Common.SrvcModel;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    public interface ISendMsgContracts
    {
        OperateStatus TemplateMsg(Guid accountID, TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters);

        OperateStatus SendTextMessage(Guid accountID, SendMessageTarget target, string message);

        OperateStatus SendArticleMessage(Guid accountID, SendMessageTarget target, IList<ArticleSendItem> message);

        OperateStatus SendNewsMesaage(Guid accountID, SendMessageTarget target, IList<NewsSendItem> message);
    }
}
