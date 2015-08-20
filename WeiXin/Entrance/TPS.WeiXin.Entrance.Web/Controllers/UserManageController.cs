using System;
using Newtonsoft.Json;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Entrance.Web.Models;
using TPS.WeiXin.Extentions.IFunction.Normal.UserManage;
using Zeus.Common.DataStatus;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Entrance.Web.Controllers
{
    public class UserManageController : BaseServiceController
    {
        /// <summary>
        /// 日志记录中的登录名
        /// </summary>
        protected override string LogonName
        {
            get { return ""; }
        }

        public ServiceResult GetByOpenID(Guid accountID, string openID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" });
            }
            var func = FunctionFactory.GetFunctionInstance<IGetUserBaseInfo>();
            var userInfo = func.GetByOpenID(account, openID);
            var jsonResult = JsonConvert.SerializeObject(userInfo);
            return new ServiceResult(jsonResult, "");
        }

        public ServiceResult CreateGroup(Guid accountID, string name)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" });
            }
            var func = FunctionFactory.GetFunctionInstance<IGroup>();
            OperateStatus status = func.CreateGroup(account, name);
            return new ServiceResult(status);
        }

        public ServiceResult MoveUser(Guid accountID, string openID, string groupID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" });
            }
            var func = FunctionFactory.GetFunctionInstance<IGroup>();
            OperateStatus status = func.MoveUser(account, openID, groupID);
            return new ServiceResult(status);
        }
    }
}