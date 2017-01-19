using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Cryptography;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Service.MixService.Controllers
{
    public class DefaultController : Controller
    {
        private readonly static Guid AccountId;
        private readonly static string TemplateId;
        private readonly static string BusinessServiceUrlFormat;

        static DefaultController()
        {
            TemplateId = ConfigurationManager.AppSettings["BindTemplateMsgId"];
            BusinessServiceUrlFormat = ConfigurationManager.AppSettings["BusinessServiceUrlFormat"];
            var accountIdStr = ConfigurationManager.AppSettings["CurrentAccountID"];
            if (Guid.TryParse(accountIdStr, out AccountId)) { }

        }

        public ActionResult Binding()
        {
            string errorMsg;
            string openId = WeiXinAuthenticate(out errorMsg);
            if (!string.IsNullOrEmpty(errorMsg) && string.IsNullOrEmpty(openId))
            {
                ViewBag.IsError = true;
                return View("Info", (object)errorMsg);
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult Bind(string tel)
        {
            string errorMsg;
            string openId = WeiXinAuthenticate(out errorMsg);
            if (!string.IsNullOrEmpty(errorMsg) && string.IsNullOrEmpty(openId))
            {
                ViewBag.IsError = true;
                return View("Info", (object)errorMsg);
            }

            var url = string.Format(BusinessServiceUrlFormat, tel, openId);
            var responseResult = HttpHelper.GetResponseResultByGet(url);
            if (responseResult.Status != ResponseStatus.Success)
            {
                ViewBag.IsError = true;
                return View("Info", (object)"服务异常，请稍后再试");
            }

            var status = JsonConvert.DeserializeObject<OperateStatus>(responseResult.ResponseString);
            if (status == null || status.ResultSign != ResultSign.Success)
            {
                ViewBag.IsError = true;
                return View("Info", (object)"绑定失败，请稍后再试");
            }

            TemplateMsgParams msgParams = new TemplateMsgParams
            {
                MsgType = EnumMsgType.Success,
                TemplateID = TemplateId,
                ToUser = openId
            };

            IList<TemplateParameter> paramList = new List<TemplateParameter>();
            paramList.Add(new TemplateParameter { Name = "first", Value = "微信账号绑定" });
            paramList.Add(new TemplateParameter { Name = "keyword1", Value = tel });
            paramList.Add(new TemplateParameter { Name = "keyword2", Value = "您已成功绑定" });
            paramList.Add(new TemplateParameter { Name = "remark", Value = "欢迎使用，我们竭诚为您服务。" });

            SendMsgServiceModel sendMsgServiceModel = new SendMsgServiceModel();
            sendMsgServiceModel.TemplateMsg(AccountId, msgParams, paramList);

            ViewBag.IsError = false;
            return View("Info", (object)"绑定成功");
        }


        private string WeiXinAuthenticate(out string errorMsg)
        {
            if (AccountId == Guid.Empty)
            {
                errorMsg = "对不起，配置有误";
                return string.Empty;
            }

            string openid;
            if (!IsPassWeiXinAuthenticate(AccountId, out openid))
            {
                errorMsg = "对不起，请稍好再试";
                return string.Empty;
            }

            errorMsg = string.Empty;
            return openid;
        }

        /// <summary>
        /// 是否通过微信认证
        /// </summary>
        /// <returns>True 通过，false 不通过</returns>
        private bool IsPassWeiXinAuthenticate(Guid accountId, out string openId)
        {
            openId = Request.Cookies["IWD"]?.Value ?? "";

            //判断是否已认证
            if (!string.IsNullOrWhiteSpace(openId))
            {
                return true;
            }

            //是否有Code
            var code = Request["code"];
            AuthenticateServiceModel model = new AuthenticateServiceModel();
            OperateStatus status;
            if (!string.IsNullOrEmpty(code) && Request["state"] == "getauthcode")
            {
                status = model.GetOpenIDByCode(accountId, code);
                if (status.ResultSign == ResultSign.Success)
                {
                    openId = status.ReturnValue;
                    Response.Cookies.Add(new HttpCookie("IWD", openId));
                    return true;
                }
            }

            status = model.GetAuthUrl(accountId, Request.Url.AbsoluteUri, EnumGetAuthType.Base);
            if (status.ResultSign == ResultSign.Success)
            {
                Response.Redirect(status.ReturnValue, true);
            }
            return false;
        }
    }
}