using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Entities.Enums;
using TPS.WeiXin.Extentions.BaseFunction.Common;
using TPS.WeiXin.Extentions.IFunction.CustomMenu;
using Zeus.Common.DataStatus;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Entrance.Web.Controllers
{
    public class CustomMenuController : BaseServiceController
    {
        /// <summary>
        /// 日志记录中的登录名
        /// </summary>
        protected override string LogonName
        {
            get { return ""; }
        }

        public ServiceResult CreateMenus(Guid accountID, IList<CustomMenu> menus)
        {
            var func = FunctionFactory.GetFunctionInstance<ICreate>();
            OperateStatus status = func.Create(accountID, menus);
            return new ServiceResult(status);
        }

        public ActionResult Test()
        {
            var first = new CustomMenu
            {
                ID = Guid.NewGuid(),
                Name = "点击跳转",
                Type = (int) EnumMenuType.View,
                Key = "",
                View_Url = "http://www.chataowanjia.com",
                Sort = 0,
                CreateTime = DateTime.Now
            };
            var second = new CustomMenu
            {
                ID = Guid.NewGuid(),
                Name = "查看菜单",
                Type = (int)EnumMenuType.Click,
                Key = "menu",
                Sort = 1,
                CreateTime = DateTime.Now
            };
            var third = new CustomMenu
            {
                ID = Guid.NewGuid(),
                Name = "二级",
                Sort = 2,
                CreateTime = DateTime.Now
            };
            var forth = new CustomMenu
            {
                ID = Guid.NewGuid(),
                Name = "三级",
                Sort = 3,
                CreateTime = DateTime.Now
            };

            var thirdFirst = new CustomMenu
            {
                ID = Guid.NewGuid(),
                ParentMenuID = third.ID,
                Name = "第一",
                Type = (int)EnumMenuType.Click,
                Key = "first",
                Sort = 0,
                CreateTime = DateTime.Now
            };
            var thirdSecond = new CustomMenu
            {
                ID = Guid.NewGuid(),
                ParentMenuID = third.ID,
                Name = "第二",
                Type = (int)EnumMenuType.Click,
                Key = "second",
                Sort = 1,
                CreateTime = DateTime.Now
            };

            var forthFirst = new CustomMenu
            {
                ID = Guid.NewGuid(),
                ParentMenuID = forth.ID,
                Name = "第一",
                Type = (int)EnumMenuType.View,
                View_Url = "http://m.chataowanjia.com",
                Sort = 0,
                CreateTime = DateTime.Now
            };
            var forthSecond = new CustomMenu
            {
                ID = Guid.NewGuid(),
                ParentMenuID = forth.ID,
                Name = "第二",
                Type = (int)EnumMenuType.View,
                View_Url = "http://www.baidu.com",
                Sort = 1,
                CreateTime = DateTime.Now
            };
            var forthThird = new CustomMenu
            {
                ID = Guid.NewGuid(),
                ParentMenuID = forth.ID,
                Name = "第三",
                Type = (int)EnumMenuType.View,
                View_Url = "http://www.sina.com",
                Sort = 2,
                CreateTime = DateTime.Now
            };

            var data = new List<CustomMenu>();
            data.Add(first);
            data.Add(second);
            data.Add(third);
            data.Add(forth);
            data.Add(thirdFirst);
            data.Add(thirdSecond);
            data.Add(forthFirst);
            data.Add(forthSecond);
            data.Add(forthThird);

            return Json(data,JsonRequestBehavior.AllowGet);
        }
    }
}