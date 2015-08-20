using System.Collections.Generic;
using TPS.WeiXin.Common.Model;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Normal.SendMsg
{
    public interface ITemplateMsg
    {
        OperateStatus SendTemplateMsg(Account currentAccount,TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters);
    }
}