using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TPS.WeiXin.Common.Model;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.BaseCorpFunction.Common;
using TPS.WeiXin.Extentions.BaseCorpFunction.Exts;
using TPS.WeiXin.Extentions.IFunction.Corp.SendMsg;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Web;

namespace TPS.WeiXin.Extentions.BaseCorpFunction
{
    public class CSendMsg : ISend
    {
        private const string SendMessageUrlFormat =
            "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";

        public OperateStatus SendTextMessage(Account currentAccount, SendMessageTarget target, string message)
        {
            // ReSharper disable once PossibleInvalidOperationException
            var sendMsg = new SendMsg(currentAccount.AgentID.Value, target, message);
            return SendMessage(currentAccount, sendMsg);
        }

        public OperateStatus SendArticleMessage(Account currentAccount, SendMessageTarget target, IList<ArticleSendItem> message)
        {
            // ReSharper disable once PossibleInvalidOperationException
            var sendMsg = new SendMsg(currentAccount.AgentID.Value, target, message);
            return SendMessage(currentAccount, sendMsg);
        }

        public OperateStatus SendNewsMesaage(Account currentAccount, SendMessageTarget target, IList<NewsSendItem> message)
        {
            // ReSharper disable once PossibleInvalidOperationException
            var sendMsg = new SendMsg(currentAccount.AgentID.Value, target, message);
            return SendMessage(currentAccount, sendMsg);
        }

        private OperateStatus SendMessage(Account currentAccount, SendMsg msg)
        {
            try
            {
                string url = string.Format(SendMessageUrlFormat, AccessTokenHelper.GetAccessToken(currentAccount));


                var param = JsonConvert.SerializeObject(msg);
                var responseResult = HttpHelper.GetResponseResultByPost(url, param, contentType: "application/json");

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
                    Message = string.Format("创建错误,错误码：{0}，错误信息：{1}", errcode, result.Value<string>("errmsg"))
                };

            }
            catch (Exception ex)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "创建异常，" + ex.Message };
            }
        }
    }
}
