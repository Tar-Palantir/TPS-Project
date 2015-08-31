using System;
using System.Collections.Generic;
using TPS.WeiXin.Common.Utility.ServiceContracts;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.WCFService
{
    public class CustomMenuService : ICustomMenuContracts
    {
        public OperateStatus CreateMenus(Guid accountID, IList<CustomMenu> menus)
        {
            CustomMenuServiceModel model = new CustomMenuServiceModel();
            return model.CreateMenus(accountID, menus);
        }
    }
}
