using System.Runtime.Serialization;

namespace TPS.WeiXin.Common.Model.Enums
{
    [DataContract]
    public enum EnumMsgType
    {
        [EnumMember]
        Normal = 0,

        [EnumMember]
        Success = 1,

        [EnumMember]
        Warning = 2,

        [EnumMember]
        Fail = 3
    }
}
