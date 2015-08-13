using System.Collections.Generic;
using TPS.WeiXin.Common.Model;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.SendMsg
{
    public interface ITemplateMsg
    {
        OperateStatus SendTemplateMsg(TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters);
    }
}