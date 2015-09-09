using System.Data.Entity;
using TPS.WeiXin.DataAccess.Entities;
using Zeus.Common.DataAccess.Implement;
using Zeus.Common.Helper.Log;

namespace TPS.WeiXin.DataAccess.Implement
{
    public class AccountRepository : Repository<Account>
    {
        private readonly DbContext _context = new WeiXinEntities();

        //public AccountRepository()
        //{
        //    _context.Database.Log = msg => FileLogHelper.WriteInfo(msg, "SqlLog");
        //}

        /// <summary>
        /// 实体数据库
        /// </summary>
        protected override DbContext Context
        {
            get { return _context; }
        }
    }
}
