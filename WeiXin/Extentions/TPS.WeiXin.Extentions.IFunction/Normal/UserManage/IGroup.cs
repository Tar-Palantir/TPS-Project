using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Normal.UserManage
{
    public interface IGroup
    {
        OperateStatus CreateGroup(Account currentAccount, string name);

        OperateStatus MoveUser(Account currentAccount, string openID, string groupID);
    }
}