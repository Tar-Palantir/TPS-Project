using System;
using System.ServiceModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    [ServiceContract]
    public interface IAuthenticateContracts
    {
        [OperationContract]
        OperateStatus GetAuthUrl(Guid accountID, string redirectUrl, EnumGetAuthType getAuthType);

        [OperationContract]
        OperateStatus GetOpenIDByCode(Guid accountID, string code);

        [OperationContract]
        OperateStatus GetUserInfoByCode(Guid accountID, string code);
    }
}
