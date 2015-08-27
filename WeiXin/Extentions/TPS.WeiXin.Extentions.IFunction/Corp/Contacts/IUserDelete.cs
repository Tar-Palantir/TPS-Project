using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Corp.Contacts
{
    public interface IUserDelete
    {
        OperateStatus Delete(Account currentAccount, string userId);
    }
}