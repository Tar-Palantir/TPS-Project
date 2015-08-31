using System;
using System.Collections.Generic;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Extentions.IFunction.Corp.SendMsg;
using TPS.WeiXin.Extentions.IFunction.Normal.SendMsg;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.Models
{
    public class SendMsgServiceModel
    {
        public OperateStatus TemplateMsg(Guid accountID, TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<ITemplateMsg>();
            OperateStatus status = func.SendTemplateMsg(currentAccount, templateMsgParams, parameters);
            return status;
        }


        public OperateStatus SendTextMessage(Guid accountID, SendMessageTarget target, string message)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<ISend>();
            OperateStatus status = func.SendTextMessage(currentAccount, target, message);
            return status;
        }

        public OperateStatus SendArticleMessage(Guid accountID, SendMessageTarget target, IList<ArticleSendItem> message)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<ISend>();
            OperateStatus status = func.SendArticleMessage(currentAccount, target, message);
            return status;
        }

        public OperateStatus SendNewsMesaage(Guid accountID, SendMessageTarget target, IList<NewsSendItem> message)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<ISend>();
            OperateStatus status = func.SendNewsMesaage(currentAccount, target, message);
            return status;
        }
    }
}