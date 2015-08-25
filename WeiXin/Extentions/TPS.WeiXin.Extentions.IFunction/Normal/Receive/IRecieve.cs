using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Normal.Receive
{
    public interface IReceive
    {
        OperateStatus Main(Account currentAccount, string signature, string timestamp, string nonce, string echostr);
    }
}