using System;
using System.Collections.Generic;
using System.ServiceModel;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Common.Utility.ServiceContracts
{
    /// <summary>
    /// 自定义菜单服务契约
    /// </summary>
    [ServiceContract]
    public interface ICustomMenuContracts
    {
        //[OperationContract]
        //OperateStatus CreateMenus(Guid accountID, IList<CustomMenu> menus);
    }
}
