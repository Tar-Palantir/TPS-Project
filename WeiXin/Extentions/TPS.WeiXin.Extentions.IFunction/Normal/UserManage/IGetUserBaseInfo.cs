using TPS.WeiXin.Common.Model;
using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;

namespace TPS.WeiXin.Extentions.IFunction.Normal.UserManage
{
    public interface IGetUserBaseInfo
    {
        UserInfo GetByOpenID(Account currentAccount, string openID);
    }
}