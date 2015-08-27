using TPS.WeiXin.Common.SrvcModel.Enums;

namespace TPS.WeiXin.Common.SrvcModel
{
    public class TemplateMsgParams
    {
        public string ToUser { get; set; }

        public string TemplateID { get; set; }

        public string Url { get; set; }

        public EnumMsgType MsgType { get; set; }
    }
}
