using System;
using Newtonsoft.Json;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.Extentions.IFunction.Normal.Authenticate;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.Models
{
    public class AuthenticateServiceModel
    {
        /// <summary>
        /// 获取认证用Url
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="redirectUrl">重定向Url</param>
        /// <param name="getAuthType">获取认证的类型</param>
        /// <returns>操作结果</returns>
        public OperateStatus GetAuthUrl(Guid accountID, string redirectUrl, EnumGetAuthType getAuthType)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IOAuth>();
            var result = func.GetAuthUrl(account, redirectUrl, getAuthType);
            return new OperateStatus {ResultSign = ResultSign.Success, ReturnValue = result};
        }

        /// <summary>
        /// 使用Code获取OpenID
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="code">Code</param>
        /// <returns>操作结果</returns>
        public OperateStatus GetOpenIDByCode(Guid accountID, string code)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
            }

            var func = FunctionFactory.GetFunctionInstance<IOAuth>();
            var result = func.GetOpenIDByCode(account, code);
            return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = result };
        }

        /// <summary>
        /// 通过Code获取用户信息
        /// </summary>
        /// <param name="accountID">账号ID</param>
        /// <param name="code">Code</param>
        /// <returns>操作结果</returns>
        public OperateStatus GetUserInfoByCode(Guid accountID, string code)
        {
            AccountServiceModel model = new AccountServiceModel();
            var account = model.GetById(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账户不存在" };
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
            return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = jsonResult };
        }
    }
}