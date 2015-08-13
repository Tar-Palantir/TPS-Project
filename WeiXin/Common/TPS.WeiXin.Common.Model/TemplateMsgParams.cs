using System;
using TPS.WeiXin.Common.Model.Enums;

namespace TPS.WeiXin.Common.Model
{
    public class TemplateMsgParams
    {
        public string ToUser { get; set; }

        public string TemplateID { get; set; }

        public string Url { get; set; }

        public EnumMsgType MsgType { get; set; }

        public Guid AccountID { get; set; }
    }
}
