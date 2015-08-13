using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Newtonsoft.Json;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Entities.Enums;
using TPS.WeiXin.DataAccess.Implement;
using TPS.WeiXin.Extentions.BaseFunction.Common;
using TPS.WeiXin.Extentions.BaseFunction.Exts;
using TPS.WeiXin.Extentions.IEvent;
using TPS.WeiXin.Extentions.IFunction.Receive;
using Zeus.Common.DataStatus;
using Zeus.Common.Helper.Cryptography;
using Zeus.Common.Helper.Log;

namespace TPS.WeiXin.Extentions.BaseFunction
{
    public sealed class BReceive : IReceive
    {
        public OperateStatus Main(Guid accountID, string signature, string timestamp, string nonce, string echostr)
        {
            AccountRepository accountServiceModel = new AccountRepository();
            var account = accountServiceModel.GetAccountByID(accountID);
            if (account == null)
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "账号不存在" };
            }

            if (!CheckSignature(account, signature, timestamp, nonce))
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "验签不通过" };
            }

            if (!string.IsNullOrEmpty(echostr))
            {
                return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = echostr };
            }

            HttpContext.Current.Request.InputStream.Position = 0;
            string content;
            using (var reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                content = reader.ReadToEnd();
            }

            FileLogHelper.WriteInfo("Content:" + content);
            if (string.IsNullOrEmpty(content))
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "请求参数内容不存在" };
            }
            var dicParams = ConvertMsg(content);
            FileLogHelper.WriteInfo(dicParams);

            if (!dicParams.ContainsKey("MsgType"))
            {
                return new OperateStatus { ResultSign = ResultSign.Failed, Message = "不包含MsgType" };
            }
            string responseStr;
            switch (dicParams["MsgType"])
            {
                case "text":
                    responseStr = GetResponseForText(dicParams);
                    break;
                case "event":
                    responseStr = GetResponseForEvent(dicParams);
                    break;
                default:
                    responseStr = "暂时不支持";
                    break;
            }
            FileLogHelper.WriteInfo("ResponseString:" + responseStr);

            return new OperateStatus { ResultSign = ResultSign.Success, ReturnValue = responseStr };

        }

        private string GetResponseForEvent(Dictionary<string, string> dicParams)
        {
            if (!dicParams.ContainsKey("Event")) throw new Exception("没有获取到Event");
            switch (dicParams["Event"])
            {
                case "CLICK":
                    {
                        if (!dicParams.ContainsKey("EventKey")) throw new Exception("没有获取到EventKey");

                        CustomMenu cMenu;
                        var clickEvents = EventListenerProvider.GetEventListener<IWeiXinClickEvent>(dicParams["EventKey"], out cMenu);

                        var events = clickEvents.Aggregate(new Action<IDictionary<string, string>, CustomMenu>((a,b) => { }),
                            (s, c) => s + c.OnEventInvoke);

                        EventHelper.EventInvoke(events, dicParams, cMenu);

                        var responseEvent = EventListenerProvider.GetSpecialEvent(clickEvents, cMenu);

                        return responseEvent.GetResponseString(dicParams, cMenu);
                    }
                case "subscribe":
                    return null;
                default:
                    return null;
            }
        }

        private string GetResponseForText(Dictionary<string, string> dicParams)
        {
            if (!dicParams.ContainsKey("ToUserName")) throw new Exception("没有获取到ToUserName");
            if (!dicParams.ContainsKey("FromUserName")) throw new Exception("没有获取到FromUserName");
            if (!dicParams.ContainsKey("Content")) throw new Exception("没有获取到Content");

            var replyRepository = new ReplyRepository();
            Reply reply = replyRepository.GetReply(dicParams["Content"], EnumKeyType.Keyword);

            BaseReply returnReply;
            switch (reply.ReplyType)
            {
                case (int)EnumReplyType.TextReply:
                    returnReply = new TextReply { Content = reply.Txt_Content };
                    break;
                case (int)EnumReplyType.ArticleReply:
                    returnReply = new ArticleReply
                    {
                        Articles = JsonConvert.DeserializeObject<List<ArticleReplyItem>>(reply.Article_Content)
                    };
                    break;
                default:
                    return null;
            }

            returnReply.FromUserName = dicParams["ToUserName"];
            returnReply.ToUserName = dicParams["FromUserName"];

            return returnReply.GetXmlString();
        }

        private bool CheckSignature(Account account, string signature, string timestamp, string nonce)
        {
            ISet<string> paramList = new SortedSet<string> { account.Token, timestamp, nonce };
            var currentSignature = HashCryptography.Sha1Encrypt(paramList.Aggregate((o, t) => o + t));
            FileLogHelper.WriteInfo("CheckSignature:" + currentSignature);
            return currentSignature == signature;
        }

        private Dictionary<string, string> ConvertMsg(string xmlMsg)
        {
            try
            {
                var sr = new StringReader(xmlMsg);
                var xElement = XElement.Load(sr);
                var nodes = xElement.Elements();
                var result = nodes.ToDictionary(node => node.Name.LocalName, node => node.Value);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("将xml内容转换为对象失败，xml内容【{0}】失败【{1}】", xmlMsg, e));
            }
        }
    }
}
