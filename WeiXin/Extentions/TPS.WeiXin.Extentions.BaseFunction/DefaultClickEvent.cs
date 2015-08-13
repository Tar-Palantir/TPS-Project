using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Entities.Enums;
using TPS.WeiXin.DataAccess.Implement;
using TPS.WeiXin.Extentions.BaseFunction.Exts;
using TPS.WeiXin.Extentions.IEvent;

namespace TPS.WeiXin.Extentions.BaseFunction
{
    public sealed class DefaultClickEvent : IWeiXinClickEvent
    {
        public string GetResponseString(IDictionary<string, string> dicParams, CustomMenu cMenu)
        {
            if (!dicParams.ContainsKey("ToUserName")) throw new Exception("没有获取到ToUserName");
            if (!dicParams.ContainsKey("FromUserName")) throw new Exception("没有获取到FromUserName");

            var replyServiceModel = new ReplyRepository();
            Reply reply = replyServiceModel.GetReply(dicParams["EventKey"], EnumKeyType.BtnClick);

            if (reply == null)
            {
                return null;
            }

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

        public void OnEventInvoke(IDictionary<string, string> args, CustomMenu cMenu) { }
    }
}
