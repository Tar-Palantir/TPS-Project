using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Corp.Contacts
{
    public interface IUserUpdate
    {
        OperateStatus Update(Account currentAccount, CorpUserInfo userInfo);
    }
}