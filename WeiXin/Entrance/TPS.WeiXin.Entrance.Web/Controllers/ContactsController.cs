using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Entrance.Web.Models;
using TPS.WeiXin.Extentions.IFunction.Corp.Contacts;
using TPS.WeiXin.Extentions.IFunction.Normal.SendMsg;
using Zeus.Common.DataStatus;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Entrance.Web.Controllers
{
    public class ContactsController : BaseServiceController
    {
        /// <summary>
        /// 日志记录中的登录名
        /// </summary>
        protected override string LogonName
        {
            get { return ""; }
        }

        #region 用户管理


        public ServiceResult CreateUser(Guid accountID, CorpUserInfo userInfo)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" });
            }

            var func = FunctionFactory.GetFunctionInstance<IUserCreate>();
            OperateStatus status = func.Create(currentAccount, userInfo);
            return new ServiceResult(status);
        }

        public ServiceResult DeleteUser(Guid accountID, string userID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" });
            }

            var func = FunctionFactory.GetFunctionInstance<IUserDelete>();
            OperateStatus status = func.Delete(currentAccount, userID);
            return new ServiceResult(status);
        }

        public ServiceResult GetUserByDeparment(Guid accountID, string departmentID, bool fetchChild, int enumStatus, bool getDetail)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" });
            }

            var func = FunctionFactory.GetFunctionInstance<IUserGetByDepartment>();
            OperateStatus status = func.GetByDepartment(currentAccount, departmentID, fetchChild, enumStatus, getDetail);
            return new ServiceResult(status);
        }

        public ServiceResult GetUserByID(Guid accountID, string userID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" });
            }

            var func = FunctionFactory.GetFunctionInstance<IUserGetByID>();
            OperateStatus status = func.GetByID(currentAccount, userID);
            return new ServiceResult(status);
        }

        public ServiceResult InviteUser(Guid accountID, string userID)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" });
            }

            var func = FunctionFactory.GetFunctionInstance<IUserInvite>();
            OperateStatus status = func.Invite(currentAccount, userID);
            return new ServiceResult(status);
        }

        public ServiceResult UpdateUser(Guid accountID, CorpUserInfo userInfo)
        {
            AccountServiceModel model = new AccountServiceModel();
            var currentAccount = model.GetById(accountID);
            if (currentAccount == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" });
            }

            var func = FunctionFactory.GetFunctionInstance<IUserUpdate>();
            OperateStatus status = func.Update(currentAccount, userInfo);
            return new ServiceResult(status);
        }

        #endregion
    }
}