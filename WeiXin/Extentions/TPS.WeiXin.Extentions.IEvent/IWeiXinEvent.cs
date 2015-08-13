using System.Collections.Generic;
using TPS.WeiXin.DataAccess.Entities;

namespace TPS.WeiXin.Extentions.IEvent
{
    public interface IWeiXinEvent
    {
        void OnEventInvoke(IDictionary<string, string> args, CustomMenu cMenu);
    }
}
