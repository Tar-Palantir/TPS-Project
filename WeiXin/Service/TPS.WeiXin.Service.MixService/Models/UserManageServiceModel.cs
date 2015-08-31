using System;
using Newtonsoft.Json;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Extentions.IFunction.Normal.UserManage;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.Models
{
    public class UserManageServiceModel
    {
        public OperateStatus GetByOpenID(Guid accountID, string openID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }
            var func = FunctionFactory.GetFunctionInstance<IGetUserBaseInfo>();
            var userInfo = func.GetByOpenID(account, openID);
            var jsonResult = JsonConvert.SerializeObject(userInfo);
            return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = jsonResult };
        }

        public OperateStatus CreateGroup(Guid accountID, string name)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }
            var func = FunctionFactory.GetFunctionInstance<IGroup>();
            OperateStatus status = func.CreateGroup(account, name);
            return status;
        }

        public OperateStatus MoveUser(Guid accountID, string openID, string groupID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }
            var func = FunctionFactory.GetFunctionInstance<IGroup>();
            OperateStatus status = func.MoveUser(account, openID, groupID);
            return status;
        }
    }
}