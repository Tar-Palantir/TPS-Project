namespace TPS.WeiXin.Extentions.BaseFunction.Exts
{
    public class TextReply : BaseReply
    {
        public string Content { set; get; }

        public override string MsgType
        {
            get { return "text"; }
        }

        public override string ExtXmlString()
        {
            return "<Content><![CDATA[" + Content + "]]></Content>";
        }
    }
}
