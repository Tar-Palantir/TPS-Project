using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.BaseFunction.Common;
using TPS.WeiXin.Extentions.BaseFunction.Exts;
using TPS.WeiXin.Extentions.IFunction.Normal.SendMsg;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseFunction
{
    public sealed class BSendMsg : ICustomerService, IGroupSend, IReply, ITemplateMsg
    {
        private const string SendTemplateMsgUrl = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=";

        public OperateStatus SendTemplateMsg(Account currentAccount, TemplateMsgParams templateMsgParams, IList<TemplateParameter> parameters)
        {
            try
            {
                string url = SendTemplateMsgUrl + AccessTokenHelper.GetAccessToken(currentAccount);

                var templateMsg = new TemplateMsg(templateMsgParams, parameters);

                var param = JsonConvert.SerializeObject(templateMsg);
                var responseResult = HttpHelper.GetResponseResultByPost(url, param);

                if (responseResult.Status != ResponseStatus.Success)
                {
                    return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送请求异常，" + responseResult.ExceptionMessages };
                }

                var result = JsonConvert.DeserializeObject<JObject>(responseResult.ResponseString);
                var errcode = result.Value<int>("errcode");
                if (errcode == 0)
                {
                    return new OperateStatus { ResultSign = ResultSign.Success };
                }
                return new OperateStatus
                {
                    ResultSign = ResultSign.Failed,
                    Message = string.Format("发送错误,错误码：{0}，错误信息：{1}", errcode, result.Value<string>("errmsg"))
                };

            }
            catch (Exception ex)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "发送异常，" + ex.Message };
            }
        }
    }
}
