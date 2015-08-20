using System;
using System.Collections.Generic;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.Model;
using TPS.WeiXin.Entrance.Web.Models;
using TPS.WeiXin.Extentions.IFunction.Normal.SendMsg;
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

        public ServiceResult TemplateMsg(Guid accountID, TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" });
            }

            var func = FunctionFactory.GetFunctionInstance<ITemplateMsg>();
            OperateStatus status = func.SendTemplateMsg(currentAccount, templateMsgParams, parameters);
            return new ServiceResult(status);
        }
    }
}