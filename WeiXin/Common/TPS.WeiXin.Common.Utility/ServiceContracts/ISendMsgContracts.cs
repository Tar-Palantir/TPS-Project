﻿using System.Collections.Generic;
using TPS.WeiXin.Common.Model;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    public interface ISendMsgContracts
    {
        OperateStatus TemplateMsg(TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters);
    }
}