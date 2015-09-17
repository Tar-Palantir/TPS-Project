using System;
using System.Data.Entity;
using System.Linq;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Entities.Enums;
using Zeus.Common.DataAccess.Implement;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.DataAccess.Implement
{
    public class CustomMenuRepository : Repository<CustomMenu>
    {
        private readonly DbContext _context = new WeiXinEntities();

        //public CustomMenuRepository()
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

        public OperateStatus Create(CustomMenu entity)
        {
            OperateStatus status = new OperateStatus { ResultSign = ResultSign.Failed };
            if (ObjectQuery.Any(p => p.Key == entity.Key && p.Type == entity.Type && p.Status == (int)StateSign.Normal))
            {
                status.Message = "添加失败，该关键字已存在";
                return status;
            }

            entity.ID = Guid.NewGuid();
            try
            {
                ObjectSet.Add(entity);
                _context.SaveChanges();

                status.ResultSign = ResultSign.Success;
                status.Message = "添加成功";
            }
            catch
            {
                status.Message = "添加失败，出现异常";
            }
            return status;
        }

        public new OperateStatus Update(CustomMenu entity)
        {
            OperateStatus status = new OperateStatus { ResultSign = ResultSign.Failed };
            var originEntity = ObjectQuery.FirstOrDefault(p => p.ID == entity.ID);
            if (originEntity == null)
            {
                status.Message = "修改失败，原数据不存在";
                return status;
            }

            originEntity.Type = entity.Type;
            originEntity.Name = entity.Name;
            originEntity.View_Url = entity.View_Url;

            try
            {
                _context.SaveChanges();

                status.ResultSign = ResultSign.Success;
                status.Message = "修改成功";
            }
            catch
            {
                status.Message = "修改失败，出现异常";
            }
            return status;
        }

        public OperateStatus UpdateStatus(Guid id, int currentStatus)
        {
            OperateStatus status = new OperateStatus { ResultSign = ResultSign.Failed };
            var originEntity = ObjectQuery.FirstOrDefault(p => p.ID == id);
            if (originEntity == null)
            {
                status.Message = "修改失败，原数据不存在";
                return status;
            }


            if (!Enum.IsDefined(typeof(StateSign), currentStatus))
            {
                status.Message = "修改失败，不存在该状态";
                return status;
            }

            originEntity.Status = currentStatus;

            try
            {
                _context.SaveChanges();

                status.ResultSign = ResultSign.Success;
                status.Message = "修改成功";
            }
            catch
            {
                status.Message = "修改失败，出现异常";
            }
            return status;
        }

        public CustomMenu GetByKey(string key)
        {
            return ObjectQuery.Where(p => p.Type == (int)EnumMenuType.Click)
                .Where(p => p.Key == key).FirstOrDefault(p => p.Status == (int)StateSign.Normal);
        }

    }
}
