using TPS.WeiXin.Common.SrvcModel.Enums;

namespace TPS.WeiXin.Common.SrvcModel
{
    /// <summary>
    /// 模板消息发送参数
    /// </summary>
    public class TemplateMsgParams
    {
        /// <summary>
        /// 接收用户，openid
        /// </summary>
        public string ToUser { get; set; }

        /// <summary>
        /// 模板ID
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        /// 跳转Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public EnumMsgType MsgType { get; set; }
    }
}
