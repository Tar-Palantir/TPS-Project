using System;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Extentions.IFunction.Corp.Contacts;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.Models
{
    public class ContactsServiceModel
    {
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns>操作结果</returns>
        public OperateStatus CreateUser(Guid accountID, CorpUserInfo userInfo)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IUserCreate>();
            OperateStatus status = func.Create(currentAccount, userInfo);
            return status;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>操作结果</returns>
        public OperateStatus DeleteUser(Guid accountID, string userID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IUserDelete>();
            OperateStatus status = func.Delete(currentAccount, userID);
            return status;
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
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IUserGetByDepartment>();
            OperateStatus status = func.GetByDepartment(currentAccount, departmentID, fetchChild, enumStatus, getDetail);
            return status;
        }

        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>操作结果，CorpUserInfo</returns>
        public OperateStatus GetUserByID(Guid accountID, string userID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IUserGetByID>();
            OperateStatus status = func.GetByID(currentAccount, userID);
            return status;
        }

        /// <summary>
        /// 邀请用户
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userID">用户ID</param>
        /// <returns>操作结果</returns>
        public OperateStatus InviteUser(Guid accountID, string userID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IUserInvite>();
            OperateStatus status = func.Invite(currentAccount, userID);
            return status;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns>操作结果</returns>
        public OperateStatus UpdateUser(Guid accountID, CorpUserInfo userInfo)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IUserUpdate>();
            OperateStatus status = func.Update(currentAccount, userInfo);
            return status;
        }
    }
}