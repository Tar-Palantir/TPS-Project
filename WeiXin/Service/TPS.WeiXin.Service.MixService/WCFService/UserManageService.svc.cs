using System;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.WCFService
{
    public class UserManageService : IUserManageContracts
    {
        public OperateStatus GetByOpenID(Guid accountID, string openID)
        {
            UserManageServiceModel model = new UserManageServiceModel();
            return model.GetByOpenID(accountID, openID);
        }

        public OperateStatus CreateGroup(Guid accountID, string name)
        {
            UserManageServiceModel model = new UserManageServiceModel();
            return model.CreateGroup(accountID, name);
        }

        public OperateStatus MoveUser(Guid accountID, string openID, string groupID)
        {
            UserManageServiceModel model = new UserManageServiceModel();
            return model.MoveUser(accountID, openID, groupID);
        }
    }
}
