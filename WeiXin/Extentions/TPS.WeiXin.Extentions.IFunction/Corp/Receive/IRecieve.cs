using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Corp.Receive
{
    public interface IReceive
    {
        OperateStatus Main(Account currentAccount, string signature, string timestamp, string nonce, string echostr);
    }
}