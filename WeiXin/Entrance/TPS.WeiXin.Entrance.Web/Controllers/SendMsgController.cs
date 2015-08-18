using System.Collections.Generic;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.Model;
using TPS.WeiXin.Extentions.IFunction.SendMsg;
using Zeus.Common.DataStatus;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Entrance.Web.Controllers
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

        public ServiceResult TemplateMsg(TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters)
        {
            var func = FunctionFactory.GetFunctionInstance<ITemplateMsg>();
            OperateStatus status = func.SendTemplateMsg(templateMsgParams, parameters);
            return new ServiceResult(status);
        }
    }
}