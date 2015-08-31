using System;
using System.ServiceModel;
using TPS.WeiXin.Common.SrvcModel;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    [ServiceContract]
    public interface IContactsContracts
    {
        [OperationContract]
        OperateStatus CreateUser(Guid accountID, CorpUserInfo userInfo);

        [OperationContract]
        OperateStatus DeleteUser(Guid accountID, string userID);

        [OperationContract]
        OperateStatus GetUserByDeparment(Guid accountID, string departmentID, bool fetchChild, int enumStatus,
            bool getDetail);

        [OperationContract]
        OperateStatus GetUserByID(Guid accountID, string userID);

        [OperationContract]
        OperateStatus InviteUser(Guid accountID, string userID);

        [OperationContract]
        OperateStatus UpdateUser(Guid accountID, CorpUserInfo userInfo);
    }
}
