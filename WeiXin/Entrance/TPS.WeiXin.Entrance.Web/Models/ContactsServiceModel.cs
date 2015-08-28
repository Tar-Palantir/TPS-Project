using System;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Extentions.IFunction.Corp.Contacts;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Entrance.Web.Models
{
    public class ContactsServiceModel
    {
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