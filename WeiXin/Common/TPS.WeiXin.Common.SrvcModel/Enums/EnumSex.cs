using System.Runtime.Serialization;

namespace TPS.WeiXin.Common.SrvcModel.Enums
{
    /// <summary>
    /// 性别枚举
    /// </summary>
    [DataContract]
    public enum EnumSex
    {
        /// <summary>
        /// 男
        /// </summary>
        [EnumMember]
        Male = 1,

        /// <summary>
        /// 女
        /// </summary>
        [EnumMember]
        Female = 2
    }
}