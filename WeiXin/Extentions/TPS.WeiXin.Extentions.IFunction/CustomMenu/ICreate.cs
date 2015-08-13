using System;
using System.Collections.Generic;
using Zeus.Common.DataStatus;
using CustonMenuEntity = TPS.WeiXin.DataAccess.Entities.CustomMenu;

namespace TPS.WeiXin.Extentions.IFunction.CustomMenu
{
    public interface ICreate
    {
        OperateStatus Create(Guid accountId, IList<CustonMenuEntity> menus);
    }
}