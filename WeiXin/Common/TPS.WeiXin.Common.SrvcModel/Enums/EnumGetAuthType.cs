using System.Runtime.Serialization;

namespace TPS.WeiXin.Common.SrvcModel.Enums
{
    [DataContract]
    public enum EnumGetAuthType
    {
        [EnumMember]
        Base = 0,

        [EnumMember]
        Detail = 1
    }
}