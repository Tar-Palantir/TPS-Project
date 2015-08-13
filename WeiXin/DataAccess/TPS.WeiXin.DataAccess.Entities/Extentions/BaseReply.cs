using System;
using System.Text;
using Zeus.Common.Helper;

namespace TPS.WeiXin.DataAccess.Entities.Extentions
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
    }
}
