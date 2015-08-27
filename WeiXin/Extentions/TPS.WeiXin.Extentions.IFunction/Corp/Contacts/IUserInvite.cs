using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Corp.Contacts
{
    public interface IUserInvite
    {
        OperateStatus Invite(Account currentAccount, string userId);
    }
}