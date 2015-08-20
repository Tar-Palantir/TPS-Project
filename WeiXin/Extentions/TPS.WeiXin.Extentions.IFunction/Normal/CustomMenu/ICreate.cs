using System;
using System.Collections.Generic;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;
using CustonMenuEntity = TPS.WeiXin.DataAccess.Entities.CustomMenu;

namespace TPS.WeiXin.Extentions.IFunction.Normal.CustomMenu
{
    public interface ICreate
    {
        OperateStatus Create(Account currentAccount, IList<CustonMenuEntity> menus);
    }
}