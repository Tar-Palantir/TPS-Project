using System;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.WCFService
{
    public class UserManageService : IUserManageContracts
    {
        /// <summary>
        /// 根据OpenID获取用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="openID">OpenID</param>
        /// <returns>操作结果，UserInfo信息</returns>
        public OperateStatus GetByOpenID(Guid accountID, string openID)
        {
            UserManageServiceModel model = new UserManageServiceModel();
            return model.GetByOpenID(accountID, openID);
        }

        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="name">分组名称</param>
        /// <returns>操作结果</returns>
        public OperateStatus CreateGroup(Guid accountID, string name)
        {
            UserManageServiceModel model = new UserManageServiceModel();
            return model.CreateGroup(accountID, name);
        }

        /// <summary>
        /// 移动用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="openID">OpenID</param>
        /// <param name="groupID">分组ID</param>
        /// <returns>操作结果</returns>
        public OperateStatus MoveUser(Guid accountID, string openID, string groupID)
        {
            UserManageServiceModel model = new UserManageServiceModel();
            return model.MoveUser(accountID, openID, groupID);
        }
    }
}
