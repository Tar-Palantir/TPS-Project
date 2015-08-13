using System.Collections.Generic;
using System.Text;

namespace TPS.WeiXin.Extentions.BaseFunction.Exts
{
    public class ArticleReply : BaseReply
    {
        public IList<ArticleReplyItem> Articles { set; get; }

        public override string MsgType
        {
            get { return "news"; }
        }

        public override string ExtXmlString()
        {
            var count = Articles.Count;
            var sb = new StringBuilder();
            sb.Append("<ArticleCount>" + count + "</ArticleCount>");
            if (count <= 0)
            {
                return sb.ToString();
            }

            sb.Append("<Articles>");
            foreach (var articleReplyItem in Articles)
            {
                sb.Append("<item>");
                sb.Append("<Title><![CDATA[" + articleReplyItem.Title + "]]></Title>");
                sb.Append("<Description><![CDATA[" + articleReplyItem.Description + "]]></Description>");
                sb.Append("<PicUrl><![CDATA[" + articleReplyItem.PicUrl + "]]></PicUrl>");
                sb.Append("<Url><![CDATA[" + articleReplyItem.Url + "]]></Url>");
                sb.Append("</item>");
            }
            sb.Append("</Articles>");

            return sb.ToString();
        }

    }

    public class ArticleReplyItem
    {
        public string Title { set; get; }

        public string Description { set; get; }

        public string PicUrl { set; get; }

        public string Url { set; get; }
    }
}
