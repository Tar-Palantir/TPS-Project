using System;
using System.ServiceModel;
using TPS.WeiXin.Common.SrvcModel;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    /// <summary>
    /// 通讯录服务契约
    /// </summary>
    [ServiceContract]
    public interface IContactsContracts
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus CreateUser(Guid accountID, CorpUserInfo userInfo);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus DeleteUser(Guid accountID, string userID);

        /// <summary>
        /// 根据部门信息获取用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="departmentID">部门ID</param>
        /// <param name="fetchChild">查询分支</param>
        /// <param name="enumStatus">查询用户状态</param>
        /// <param name="getDetail">获取详情</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus GetUserByDeparment(Guid accountID, string departmentID, bool fetchChild, int enumStatus,
            bool getDetail);

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>操作结果，CorpUserInfo</returns>
        [OperationContract]
        OperateStatus GetUserByID(Guid accountID, string userID);

        /// <summary>
        /// 邀请用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus InviteUser(Guid accountID, string userID);

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus UpdateUser(Guid accountID, CorpUserInfo userInfo);
    }
}
