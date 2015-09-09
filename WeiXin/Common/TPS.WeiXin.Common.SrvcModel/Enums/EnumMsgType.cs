using System.Runtime.Serialization;

namespace TPS.WeiXin.Common.SrvcModel.Enums
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    [DataContract]
    public enum EnumMsgType
    {
        /// <summary>
        /// 普通消息
        /// </summary>
        [EnumMember]
        Normal = 0,

        /// <summary>
        /// 成功消息
        /// </summary>
        [EnumMember]
        Success = 1,

        /// <summary>
        /// 警告消息
        /// </summary>
        [EnumMember]
        Warning = 2,

        /// <summary>
        /// 失败消息
        /// </summary>
        [EnumMember]
        Fail = 3
    }
}
