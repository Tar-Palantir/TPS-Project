using System;
using System.Data.Entity;
using System.Linq;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataAccess.Implement;

namespace TPS.WeiXin.DataAccess.Implement
{
    public class AccountRepository : Repository<Account>
    {
        private readonly DbContext _context = new WeiXinEntities();

        /// <summary>
        /// 实体数据库
        /// </summary>
        protected override DbContext Context
        {
            get { return _context; }
        }
        public Account GetAccountByID(Guid accountId)
        {
            return ObjectQuery.FirstOrDefault(p => p.ID == accountId);
        }
    }
}
