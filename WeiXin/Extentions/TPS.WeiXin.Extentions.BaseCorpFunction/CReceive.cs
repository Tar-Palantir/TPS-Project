using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TPS.WeiXin.Common.Helper;
using TPS.WeiXin.Common.Model;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Entities.Enums;
using TPS.WeiXin.DataAccess.Implement;
using TPS.WeiXin.Extentions.BaseCorpFunction.Common;
using TPS.WeiXin.Extentions.BaseCorpFunction.Exts;
using TPS.WeiXin.Extentions.IEvent;
using TPS.WeiXin.Extentions.IFunction.Corp.Receive;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Log;

namespace TPS.WeiXin.Extentions.BaseCorpFunction
{
    public sealed class CReceive : IReceive
    {
        public OperateStatus Main(Account currentAccount, string signature, string timestamp, string nonce, string echostr)
        {
            WXBizMsgCryptHelper wxcrptyHelper = new WXBizMsgCryptHelper();
            string replyEchoStr;
            var ret = wxcrptyHelper.VerifyURL(currentAccount, signature, timestamp, nonce, echostr, out replyEchoStr);
            if (ret != EnumWXBizMsgCryptErrorCode.OK)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "验签不通过" };
            }

            if (!string.IsNullOrEmpty(replyEchoStr))
            {
                return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = replyEchoStr };
            }

            HttpContext.Current.Request.InputStream.Position = 0;
            string content;
            using (var reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                content = reader.ReadToEnd();
            }

            FileLogHelper.WriteInfo("Content:" + content, "CorpInfoLog");
            if (string.IsNullOrEmpty(content))
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "请求参数内容不存在" };
            }
            
            string deContent;
            ret = wxcrptyHelper.DecryptMsg(currentAccount, signature, timestamp, nonce, content, out deContent);
            if (ret != EnumWXBizMsgCryptErrorCode.OK)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "解密不通过" };
            }
            FileLogHelper.WriteInfo("DeCentent:" + deContent, "CorpInfoLog");

            var dicParams = XmlHelper.ConvertToDictionary(deContent);
            FileLogHelper.WriteInfo(dicParams, "CorpInfoLog");

            if (!dicParams.ContainsKey("MsgType"))
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "不包含MsgType" };
            }
            string responseStr;
            switch (dicParams["MsgType"])
            {
                case "text":
                    responseStr = GetResponseForText(currentAccount, dicParams);
                    break;
                case "event":
                    responseStr = GetResponseForEvent(currentAccount, dicParams);
                    break;
                default:
                    responseStr = "暂时不支持";
                    break;
            }
            FileLogHelper.WriteInfo("ResponseString:" + responseStr, "CorpInfoLog");

            string enResponseStr;
            ret = wxcrptyHelper.EncryptMsg(currentAccount, responseStr, timestamp, nonce,
                out enResponseStr);
            if (ret != EnumWXBizMsgCryptErrorCode.OK)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "加密不通过" };
            }

            return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = enResponseStr };
        }

        private string GetResponseForEvent(Account currentAccount, Dictionary<string, string> dicParams)
        {
            if (!dicParams.ContainsKey("Event")) throw new Exception("没有获取到Event");
            switch (dicParams["Event"])
            {
                case "CLICK":
                    {
                        if (!dicParams.ContainsKey("EventKey")) throw new Exception("没有获取到EventKey");

                        Reply reply;
                        var clickEvents = EventListenerProvider.GetEventListener<IWeiXinClickEvent>(currentAccount.ID, dicParams["EventKey"], out reply);

                        var events = clickEvents.Aggregate(new Action<IDictionary<string, string>, Reply>((a, b) => { }),
                            (s, c) => s + c.OnEventInvoke);

                        EventHelper.EventInvoke(events, dicParams, reply);

                        var responseEvent = EventListenerProvider.GetSpecialEvent(clickEvents, reply);

                        return responseEvent.GetResponseString(dicParams, reply);
                    }
                case "subscribe":
                    {
                        Reply reply;
                        var sEvents = EventListenerProvider.GetEventListener<IWeiXinSubscribeEvent>(currentAccount.ID, "subscribe", out reply);

                        if (sEvents != null)
                        {
                            var events = sEvents.Aggregate(
                                new Action<IDictionary<string, string>, Reply>((a, b) => { }),
                                (s, c) => s + c.OnEventInvoke);
                            EventHelper.EventInvoke(events, dicParams, reply);
                        }
                        return string.Empty;
                    }
                case "unsubscribe":
                    {
                        var sEvents = EventListenerProvider.GetEventListener<IWeiXinUnsubscribeEvent>(currentAccount.ID);

                        if (sEvents != null)
                        {
                            var events = sEvents.Aggregate(
                                new Action<IDictionary<string, string>, Reply>((a, b) => { }),
                                (s, c) => s + c.OnEventInvoke);
                            EventHelper.EventInvoke(events, dicParams, null);
                        }
                        return string.Empty;
                    }
                case "SCAN":
                    return null;
                case "LOCATION":
                    return null;
                default:
                    return null;
            }
        }

        private string GetResponseForText(Account currentAccount, Dictionary<string, string> dicParams)
        {
            if (!dicParams.ContainsKey("ToUserName")) throw new Exception("没有获取到ToUserName");
            if (!dicParams.ContainsKey("FromUserName")) throw new Exception("没有获取到FromUserName");
            if (!dicParams.ContainsKey("Content")) throw new Exception("没有获取到Content");

            var replyRepository = new ReplyRepository();
            Reply reply = replyRepository.GetReply(currentAccount.ID, dicParams["Content"], EnumKeyType.Keyword);

            BaseReply returnReply;
            switch (reply.Message.Type)
            {
                case (int)EnumReplyType.TextReply:
                    returnReply = new TextReply { Content = reply.Message.Content };
                    break;
                case (int)EnumReplyType.ArticleReply:
                    returnReply = new ArticleReply
                    {
                        Articles = JsonConvert.DeserializeObject<List<ArticleReplyItem>>(reply.Message.Content)
                    };
                    break;
                default:
                    return null;
            }

            returnReply.FromUserName = dicParams["ToUserName"];
            returnReply.ToUserName = dicParams["FromUserName"];

            return returnReply.GetXmlString();
        }
    }
}
