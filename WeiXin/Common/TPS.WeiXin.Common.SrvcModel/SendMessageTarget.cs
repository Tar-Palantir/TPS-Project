using System.Collections.Generic;

namespace TPS.WeiXin.Common.SrvcModel
{
    /// <summary>
    /// 发送消息目标
    /// </summary>
    public class SendMessageTarget
    {
        /// <summary>
        /// 用户ID集合
        /// </summary>
        public IList<string> UserIDList { get; set; }

        /// <summary>
        /// 部门ID集合
        /// </summary>
        public IList<string> DepartmentIDList { get; set; }

        /// <summary>
        /// 标签ID集合
        /// </summary>
        public IList<string> TagIDLIst { get; set; }

        /// <summary>
        /// 是否发送给所有人
        /// </summary>
        public bool IsSendAll { get; set; }

    }
}