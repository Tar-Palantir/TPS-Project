using TPS.WeiXin.Common.SrvcModel.Enums;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.Extentions.IFunction.Corp.Contacts
{
    public interface IUserGetByDepartment
    {
        OperateStatus GetByDepartment(Account currentAccount, string departmentId, bool fetchChild, int enumStatus, bool getDetail);
    }
}