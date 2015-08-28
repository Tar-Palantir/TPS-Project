using System;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Entrance.Web.Models;
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
            ContactsServiceModel model = new ContactsServiceModel();
            OperateStatus status = model.CreateUser(accountID, userInfo);
            return new ServiceResult(status);
        }

        public ServiceResult DeleteUser(Guid accountID, string userID)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            OperateStatus status = model.DeleteUser(accountID, userID);
            return new ServiceResult(status);
        }

        public ServiceResult GetUserByDeparment(Guid accountID, string departmentID, bool fetchChild, int enumStatus, bool getDetail)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            OperateStatus status = model.GetUserByDeparment(accountID, departmentID, fetchChild, enumStatus, getDetail);
            return new ServiceResult(status);
        }

        public ServiceResult GetUserByID(Guid accountID, string userID)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            OperateStatus status = model.GetUserByID(accountID, userID);
            return new ServiceResult(status);
        }

        public ServiceResult InviteUser(Guid accountID, string userID)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            OperateStatus status = model.InviteUser(accountID, userID);
            return new ServiceResult(status);
        }

        public ServiceResult UpdateUser(Guid accountID, CorpUserInfo userInfo)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            OperateStatus status = model.UpdateUser(accountID, userInfo);
            return new ServiceResult(status);
        }

        #endregion
    }
}