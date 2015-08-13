﻿using System;
using System.Data.Entity;
using System.Linq;
using TPS.WeiXin.DataAccess.Entities;
using TPS.WeiXin.DataAccess.Entities.Enums;
using Zeus.Common.DataAccess.Implement;
using Zeus.Common.DataStatus;

namespace TPS.WeiXin.DataAccess.Implement
{
    public class ReplyRepository : Repository<Reply>
    {
        private readonly DbContext _context = new WeiXinEntities();

        /// <summary>
        /// 实体数据库
        /// </summary>
        protected override DbContext Context
        {
            get { return _context; }
        }

        public OperateStatus Create(Reply entity)
        {
            OperateStatus status = new OperateStatus { ResultSign = ResultSign.Failed };
            if (ObjectQuery.Any(p => p.Key == entity.Key && p.KeyType == entity.KeyType && p.Status == (int)StateSign.Normal))
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

        public new OperateStatus Update(Reply entity)
        {
            OperateStatus status = new OperateStatus { ResultSign = ResultSign.Failed };
            var originEntity = ObjectQuery.FirstOrDefault(p => p.ID == entity.ID);
            if (originEntity == null)
            {
                status.Message = "修改失败，原数据不存在";
                return status;
            }

            originEntity.ReplyType = entity.ReplyType;
            originEntity.Txt_Content = entity.Txt_Content;
            originEntity.Article_Content = entity.Article_Content;

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

        public Reply GetReply(string key, EnumKeyType keyType)
        {
            return ObjectQuery.Where(p => p.KeyType == (int)keyType)
                .Where(p => p.Key == key).FirstOrDefault(p => p.Status == (int)StateSign.Normal);
        }
    }
}