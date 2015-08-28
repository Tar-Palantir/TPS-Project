using System.Collections.Generic;

namespace TPS.WeiXin.Common.SrvcModel
{
    public class SendMessageTarget
    {
        public IList<string> UserIDList { get; set; }

        public IList<string> DepartmentIDList { get; set; }

        public IList<string> TagIDLIst { get; set; }

        public bool IsSendAll { get; set; }

    }
}