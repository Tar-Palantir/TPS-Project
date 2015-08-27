using System;

namespace TPS.WeiXin.Common.SrvcModel.Enums
{
    [Flags]
    public enum EnumGetDepartmentUserStatus
    {
        All = 0,

        Subscribe = 0x1,

        Forbidden = 0x2,

        Unsubscribe = 0x4
    }
}