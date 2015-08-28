using System.Collections.Generic;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Corp.SendMsg
{
    public interface ISend
    {
        OperateStatus SendTextMessage(Account currentAccount, SendMessageTarget target, string message);

        OperateStatus SendArticleMessage(Account currentAccount, SendMessageTarget target, IList<ArticleSendItem> message);

        OperateStatus SendNewsMesaage(Account currentAccount, SendMessageTarget target, IList<NewsSendItem> message);
    }
}