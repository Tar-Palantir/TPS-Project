using System;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Receive
{
    public interface IReceive
    {
        OperateStatus Main(Guid accountID, string signature, string timestamp, string nonce, string echostr);
    }
}