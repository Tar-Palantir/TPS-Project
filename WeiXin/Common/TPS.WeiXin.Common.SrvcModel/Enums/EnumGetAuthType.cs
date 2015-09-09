using System.Runtime.Serialization;

namespace TPS.WeiXin.Common.SrvcModel.Enums
{
    /// <summary>
    /// 获取认证类型枚举
    /// </summary>
    [DataContract]
    public enum EnumGetAuthType
    {
        /// <summary>
        /// 获取基本信息
        /// </summary>
        [EnumMember]
        Base = 0,

        /// <summary>
        /// 获取详情
        /// </summary>
        [EnumMember]
        Detail = 1
    }
}