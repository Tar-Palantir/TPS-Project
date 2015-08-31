using System;
using System.Collections.Generic;
using System.ServiceModel;
using TPS.WeiXin.Common.Model;
using TPS.WeiXin.Common.SrvcModel;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    [ServiceContract]
    public interface ISendMsgContracts
    {
        [OperationContract]
        OperateStatus TemplateMsg(Guid accountID, TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters);

        [OperationContract]
        OperateStatus SendTextMessage(Guid accountID, SendMessageTarget target, string message);

        [OperationContract]
        OperateStatus SendArticleMessage(Guid accountID, SendMessageTarget target, IList<ArticleSendItem> message);

        [OperationContract]
        OperateStatus SendNewsMesaage(Guid accountID, SendMessageTarget target, IList<NewsSendItem> message);
    }
}
