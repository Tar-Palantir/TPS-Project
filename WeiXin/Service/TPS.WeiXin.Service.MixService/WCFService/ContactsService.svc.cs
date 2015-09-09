using System;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.WCFService
{
    public class ContactsService : IContactsContracts
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns>操作结果</returns>
        public OperateStatus CreateUser(Guid accountID, CorpUserInfo userInfo)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.CreateUser(accountID, userInfo);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>操作结果</returns>
        public OperateStatus DeleteUser(Guid accountID, string userID)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.DeleteUser(accountID, userID);
        }

        /// <summary>
        /// 根据部门信息获取用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="departmentID">部门ID</param>
        /// <param name="fetchChild">查询分支</param>
        /// <param name="enumStatus">查询用户状态</param>
        /// <param name="getDetail">获取详情</param>
        /// <returns>操作结果</returns>
        public OperateStatus GetUserByDeparment(Guid accountID, string departmentID, bool fetchChild, int enumStatus, bool getDetail)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.GetUserByDeparment(accountID, departmentID, fetchChild, enumStatus, getDetail);
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>操作结果，CorpUserInfo</returns>
        public OperateStatus GetUserByID(Guid accountID, string userID)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.GetUserByID(accountID, userID);
        }

        /// <summary>
        /// 邀请用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>操作结果</returns>
        public OperateStatus InviteUser(Guid accountID, string userID)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.InviteUser(accountID, userID);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns>操作结果</returns>
        public OperateStatus UpdateUser(Guid accountID, CorpUserInfo userInfo)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.UpdateUser(accountID, userInfo);
        }
    }
}
