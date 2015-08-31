using System;
using System.Collections.Generic;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.IFunction.Corp.CustomMenu;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.Models
{
    public class CustomMenuServiceModel
    {
        public OperateStatus CreateMenus(Guid accountID, IList<CustomMenu> menus)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }
            OperateStatus status;
            if (account.IsCorp)
            {
                var func = FunctionFactory.GetFunctionInstance<ICreate>();
                status = func.Create(account, menus);
            }
            else
            {
                var func = FunctionFactory.GetFunctionInstance<Extentions.IFunction.Normal.CustomMenu.ICreate>();
                status = func.Create(account, menus);
            }

            return status;
        }
    }
}