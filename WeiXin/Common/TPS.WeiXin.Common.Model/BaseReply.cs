using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Entities.Enums;
using Zeus.Common.Helper;

namespace TPS.WeiXin.Common.Model
{
    public abstract class BaseReply
    {
        public string ToUserName { set; get; }

        public string FromUserName { set; get; }

        public abstract string MsgType { get; }

        public string GetXmlString()
        {
            var sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>");
            sb.Append("<CreateTime>" + DateTime.Now.ToTimestamp() + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[" + MsgType + "]]></MsgType>");
            sb.Append(ExtXmlString());
            sb.Append("</xml>");
            return sb.ToString();
        }

        public abstract string ExtXmlString();

        public static BaseReply ConvertReply(IDictionary<string, string> dicParams, Reply reply)
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
            return returnReply;
        }
    }
}
