using TPS.WeiXin.Common.SrvcModel;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Corp.Contacts
{
    public interface IUserCreate
    {
        OperateStatus Create(Account currentAccount, CorpUserInfo userInfo);
    }
}