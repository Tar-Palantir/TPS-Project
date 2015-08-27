using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Corp.Contacts
{
    public interface IUserGetByID
    {
        OperateStatus GetByID(Account currentAccount, string userId);
    }
}