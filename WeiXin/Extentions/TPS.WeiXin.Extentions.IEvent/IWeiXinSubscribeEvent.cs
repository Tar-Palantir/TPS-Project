using System.Collections.Generic;
using TPS.WeiXin.DataAccess.Entities;

namespace TPS.WeiXin.Extentions.IEvent
{
    public interface IWeiXinSubscribeEvent : IWeiXinEvent
    {
        string GetResponseString(IDictionary<string, string> dicParams, Reply reply);
    }
}