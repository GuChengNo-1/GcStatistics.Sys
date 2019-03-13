using GcStatistics.Sys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GcStatistics.Sys.Dal
{
    public interface IRepository<TEntity>
        where TEntity : EntityBase
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">model</param>
        void Insert(TEntity entity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">model</param>
        /// <param name="excludeFields"></param>
        void Update(TEntity entity, params string[] excludeFields);
        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity">model</param>
        void Delete(TEntity entity);

        TEntity GetEntityById(object id);

        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> where = null);

        IEnumerable<TEntity> GetPageList(Expression<Func<TEntity, bool>> where = null,string order = "", int pageIndex = 1, int pageSize = 10);

        TEntity GetFirst(Expression<Func<TEntity, bool>> where = null);

        int GetCount(Expression<Func<TEntity, bool>> where = null);
    }
}
