using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Entities.Enums;
using TPS.WeiXin.Extentions.IEvent;

namespace TPS.WeiXin.Common.Model
{
    public sealed class DefaultClickEvent : IWeiXinClickEvent
    {
        public string GetResponseString(IDictionary<string, string> dicParams, Reply reply)
        {
            if (!dicParams.ContainsKey("ToUserName")) throw new Exception("没有获取到ToUserName");
            if (!dicParams.ContainsKey("FromUserName")) throw new Exception("没有获取到FromUserName");
            
            if (reply == null)
            {
                return null;
            }

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

        public void OnEventInvoke(IDictionary<string, string> args, Reply reply) { }
    }
}
