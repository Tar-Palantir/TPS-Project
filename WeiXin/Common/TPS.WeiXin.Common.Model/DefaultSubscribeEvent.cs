using System.Collections.Generic;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.Extentions.IEvent;

namespace TPS.WeiXin.Common.Model
{
    public sealed class DefaultSubscribeEvent : IWeiXinSubscribeEvent
    {
        public string GetResponseString(IDictionary<string, string> dicParams, Reply reply)
        {
            BaseReply returnReply = BaseReply.ConvertReply(dicParams, reply);
            return returnReply == null ? string.Empty : returnReply.GetXmlString();
        }

        public void OnEventInvoke(IDictionary<string, string> args, Reply reply) { }
    }
}
