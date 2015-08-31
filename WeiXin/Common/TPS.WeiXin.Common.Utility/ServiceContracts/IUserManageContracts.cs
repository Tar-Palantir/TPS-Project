using System;
using System.ServiceModel;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    [ServiceContract]
    public interface IUserManageContracts
    {
        [OperationContract]
        OperateStatus GetByOpenID(Guid accountID, string openID);

        [OperationContract]
        OperateStatus CreateGroup(Guid accountID, string name);

        [OperationContract]
        OperateStatus MoveUser(Guid accountID, string openID, string groupID);
    }
}