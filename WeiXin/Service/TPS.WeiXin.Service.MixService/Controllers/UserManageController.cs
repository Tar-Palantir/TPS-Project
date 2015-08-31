using System;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Service.MixService.Controllers
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
            UserManageServiceModel model = new UserManageServiceModel();
            OperateStatus status = model.GetByOpenID(accountID, openID);
            return new ServiceResult(status);
        }

        public ServiceResult CreateGroup(Guid accountID, string name)
        {
            UserManageServiceModel model = new UserManageServiceModel();
            OperateStatus status = model.CreateGroup(accountID, name);
            return new ServiceResult(status);
        }

        public ServiceResult MoveUser(Guid accountID, string openID, string groupID)
        {
            UserManageServiceModel model = new UserManageServiceModel();
            OperateStatus status = model.MoveUser(accountID, openID, groupID);
            return new ServiceResult(status);
        }
    }
}