using System;

namespace TPS.WeiXin.Common.SrvcModel.Enums
{
    /// <summary>
    /// 获取部门用户状态枚举,可以使用位运算
    /// </summary>
    [Flags]
    public enum EnumGetDepartmentUserStatus
    {
        /// <summary>
        /// 获取全部
        /// </summary>
        All = 0,

        /// <summary>
        /// 获取已关注的
        /// </summary>
        Subscribe = 0x1,

        /// <summary>
        /// 获取已冻结的
        /// </summary>
        Forbidden = 0x2,

        /// <summary>
        /// 获取未关注的
        /// </summary>
        Unsubscribe = 0x4
    }
}