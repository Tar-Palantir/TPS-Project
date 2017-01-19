using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.Service.MixService.Models;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Service.MixService.Controllers
{
    public class DefaultController : Controller
    {
        private readonly Guid _accountId;
        private readonly string _templateId;

        public DefaultController()
        {
            _templateId = ConfigurationManager.AppSettings["BindTemplateMsgId"];
            var accountIdStr = ConfigurationManager.AppSettings["CurrentAccountID"];
            if (Guid.TryParse(accountIdStr, out _accountId)) { }

        }

        public ActionResult Binding()
        {
            string errorMsg;
            string openId = WeiXinAuthenticate(out errorMsg);
            if (!string.IsNullOrEmpty(errorMsg) && string.IsNullOrEmpty(openId))
            {
                return View("Info", (object)errorMsg);
            }

            return View();

            //JsSDKServiceModel jsModel = new JsSDKServiceModel();
            //// ReSharper disable once PossibleNullReferenceException
            //var status = jsModel.GetJsData(accountId, Request.Url.AbsoluteUri);
            //if (status.ResultSign != ResultSign.Success)
            //{
            //    return View("Error",(object)"对不起，获取微信账号信息有误");
            //}
            //var weixinJsData = JsonConvert.DeserializeObject<WeiXinJsData>(status.ReturnValue);

            //return View(weixinJsData);
        }

        [HttpPost]
        public ActionResult Bind(string tel)
        {
            string errorMsg;
            string openId = WeiXinAuthenticate(out errorMsg);
            if (!string.IsNullOrEmpty(errorMsg) && string.IsNullOrEmpty(openId))
            {
                return View("Info", (object)errorMsg);
            }

            //TODO 调用手机号绑定接口

            TemplateMsgParams msgParams = new TemplateMsgParams
            {
                MsgType = EnumMsgType.Success,
                TemplateID = _templateId,
                ToUser = openId
            };

            IList<TemplateParameter> paramList = new List<TemplateParameter>();
            paramList.Add(new TemplateParameter { Name = "first", Value = "微信" });
            paramList.Add(new TemplateParameter { Name = "keyword1", Value = tel });
            paramList.Add(new TemplateParameter { Name = "keyword2", Value = "您已成功绑定" });
            paramList.Add(new TemplateParameter { Name = "remark", Value = "欢迎使用，我们竭诚为您服务。" });

            SendMsgServiceModel sendMsgServiceModel = new SendMsgServiceModel();
            sendMsgServiceModel.TemplateMsg(_accountId, msgParams, paramList);

            return View("Info", (object)"绑定成功");
        }


        private string WeiXinAuthenticate(out string errorMsg)
        {
            if (_accountId == Guid.Empty)
            {
                errorMsg = "对不起，配置有误";
                return string.Empty;
            }

            string openid;
            if (!IsPassWeiXinAuthenticate(_accountId, out openid))
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