using System;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Implement;

namespace TPS.WeiXin.Service.MixService.Models
{
    public class AccountServiceModel
    {
        public Account GetById(Guid id)
        {
            AccountRepository repository=new AccountRepository();
            return repository.GetById(id);
        }
    }
}