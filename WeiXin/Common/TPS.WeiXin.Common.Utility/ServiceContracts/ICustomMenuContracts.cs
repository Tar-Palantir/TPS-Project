using System;
using System.Collections.Generic;
using System.ServiceModel;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    [ServiceContract]
    public interface ICustomMenuContracts
    {
        [OperationContract]
        OperateStatus CreateMenus(Guid accountID, IList<CustomMenu> menus);
    }
}
