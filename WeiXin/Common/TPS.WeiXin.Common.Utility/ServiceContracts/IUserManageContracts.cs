using System;
using System.ServiceModel;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    /// <summary>
    /// 用户管理服务契约
    /// </summary>
    [ServiceContract]
    public interface IUserManageContracts
    {
        /// <summary>
        /// 根据OpenID获取用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="openID">OpenID</param>
        /// <returns>操作结果，UserInfo信息</returns>
        [OperationContract]
        OperateStatus GetByOpenID(Guid accountID, string openID);

        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="name">分组名称</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus CreateGroup(Guid accountID, string name);

        /// <summary>
        /// 移动用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="openID">OpenID</param>
        /// <param name="groupID">分组ID</param>
        /// <returns>操作结果</returns>
        [OperationContract]
        OperateStatus MoveUser(Guid accountID, string openID, string groupID);
    }
}