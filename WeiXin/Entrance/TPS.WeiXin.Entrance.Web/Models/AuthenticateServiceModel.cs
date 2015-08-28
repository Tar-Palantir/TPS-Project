using System;
using Newtonsoft.Json;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.Extentions.IFunction.Normal.Authenticate;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Entrance.Web.Models
{
    public class AuthenticateServiceModel
    {
        public OperateStatus GetAuthUrl(Guid accountID, string redirectUrl, EnumGetAuthType getAuthType)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IOAuth>();
            var result = func.GetAuthUrl(account, redirectUrl, getAuthType);
            return new OperateStatus {ResultSign = ResultSign.Success, ReturnValue = result};
        }

        public OperateStatus GetOpenIDByCode(Guid accountID, string code)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IOAuth>();
            var result = func.GetOpenIDByCode(account, code);
            return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = result };
        }

        public OperateStatus GetUserInfoByCode(Guid accountID, string code)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }
            UserInfo userInfo;
            if (account.IsCorp)
            {
                var func = FunctionFactory.GetFunctionInstance<IOAuth>();
                userInfo = func.GetUserInfoByCode(account, code);
            }
            else
            {
                var func = FunctionFactory.GetFunctionInstance<Extentions.IFunction.Corp.Authenticate.IOAuth>();
                userInfo = func.GetUserInfoByCode(account, code);
            }
            var jsonResult = JsonConvert.SerializeObject(userInfo);
            return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = jsonResult };
        }
    }
}