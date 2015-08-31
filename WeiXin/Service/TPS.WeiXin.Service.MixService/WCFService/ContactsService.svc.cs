using System;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.WCFService
{
    public class ContactsService : IContactsContracts
    {
        public OperateStatus CreateUser(Guid accountID, CorpUserInfo userInfo)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.CreateUser(accountID, userInfo);
        }

        public OperateStatus DeleteUser(Guid accountID, string userID)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.DeleteUser(accountID, userID);
        }

        public OperateStatus GetUserByDeparment(Guid accountID, string departmentID, bool fetchChild, int enumStatus, bool getDetail)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.GetUserByDeparment(accountID, departmentID, fetchChild, enumStatus, getDetail);
        }

        public OperateStatus GetUserByID(Guid accountID, string userID)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.GetUserByID(accountID, userID);
        }

        public OperateStatus InviteUser(Guid accountID, string userID)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.InviteUser(accountID, userID);
        }

        public OperateStatus UpdateUser(Guid accountID, CorpUserInfo userInfo)
        {
            ContactsServiceModel model = new ContactsServiceModel();
            return model.UpdateUser(accountID, userInfo);
        }
    }
}
