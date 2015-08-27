using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Entrance.Web.Models;
using TPS.WeiXin.Extentions.IFunction.Corp.CustomMenu;
using TPS.WeiXin.Extentions.IFunction.Normal.Authenticate;
using Zeus.Common.DataStatus;
using Zeus.Common.Service.MCService;

namespace TPS.WeiXin.Entrance.Web.Controllers
{
    public class AuthenticateController : BaseServiceController
    {
        /// <summary>
        /// 日志记录中的登录名
        /// </summary>
        protected override string LogonName
        {
            get { return ""; }
        }

        public ServiceResult GetAuthUrl(Guid accountID, string redirectUrl, EnumGetAuthType getAuthType)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" });
            }

            var func = FunctionFactory.GetFunctionInstance<IOAuth>();
            var result = func.GetAuthUrl(account, redirectUrl, getAuthType);
            return new ServiceResult(result, "");
        }

        public ServiceResult GetOpenIDByCode(Guid accountID, string code)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" });
            }

            var func = FunctionFactory.GetFunctionInstance<IOAuth>();
            var result = func.GetOpenIDByCode(account, code);
            return new ServiceResult(result, "");
        }

        public ServiceResult GetUserInfoByCode(Guid accountID, string code)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" });
            }
            UserInfo userInfo;
            if (account.IsCorp)
            {
                var func = FunctionFactory.GetFunctionInstance<IOAuth>();
                userInfo = func.GetUserInfoByCode(account, code);
            }
            else
            {
                var func = FunctionFactory.GetFunctionInstance<Extentions.IFunction.Corp.Authenticate.IOAuth>();
                userInfo = func.GetUserInfoByCode(account, code);
            }
            var jsonResult = JsonConvert.SerializeObject(userInfo);
            return new ServiceResult(jsonResult, "");
        }

        public ServiceResult CreateMenus(Guid accountID, IList<CustomMenu> menus)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new ServiceResult(new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" });
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

            return new ServiceResult(status);
        }
    }
}