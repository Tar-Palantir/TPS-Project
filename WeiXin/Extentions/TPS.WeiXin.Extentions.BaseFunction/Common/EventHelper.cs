using System;
using Zeus.Common.Helper.Log;

namespace TPS.WeiXin.Extentions.BaseFunction.Common
{
    /// <summary>
    /// 事件帮助类
    /// </summary>
    public class EventHelper
    {
        /// <summary>
        /// 事件调用
        /// </summary>
        /// <param name="delegate">事件委托</param>
        /// <param name="invokeParams">调用参数</param>
        public static void EventInvoke(Delegate @delegate, params object[] invokeParams)
        {
            //Parallel.ForEach(@delegate.GetInvocationList(), p =>
            foreach (var p in @delegate.GetInvocationList())
            {
                try
                {
                    p.DynamicInvoke(invokeParams);
                }
                catch (Exception ex)
                {
                    FileLogHelper.WriteError("", ex, "EventExceptionLog");
                }
            }
            //});
        }
    }
}
