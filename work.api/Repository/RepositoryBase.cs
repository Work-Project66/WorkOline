using Chloe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace work.api.Repository
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        public DataBaseTypeEnum Dbtype { get; set; }

        public DB DbName { get; set; }

        public IDbContext DbContext => new DbContextFactory(Dbtype, DbName, true).Instance;
        public IDbContext DbWriteContext => new DbContextFactory(Dbtype, DbName, false).Instance;
        public RepositoryBase(DataBaseTypeEnum dbtype, DB dbName, bool isdBMS = true)
        {
            Dbtype = dbtype;
            DbName = dbName;
        }

        #region 对象

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public IQuery<TEntity> Query(DataBaseWriteTypeEnum type = DataBaseWriteTypeEnum.Read)
        {
            if (type == DataBaseWriteTypeEnum.Read)
            {
                return DbContext.Query<TEntity>();
            }
            return DbWriteContext.Query<TEntity>();
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IQuery<TEntity> Query(Expression<Func<TEntity, bool>> expression, DataBaseWriteTypeEnum type = DataBaseWriteTypeEnum.Read)
        {
            if (type == DataBaseWriteTypeEnum.Read)
            {
                return DbContext.Query<TEntity>().Where(expression);
            }
            return DbWriteContext.Query<TEntity>().Where(expression);
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public TEntity FindByPrimaryKey(object primaryKey)
        {
            return DbContext.QueryByKey<TEntity>(primaryKey);
        }

        /// <summary>
        /// 查询列表中第一个条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, DataBaseWriteTypeEnum type = DataBaseWriteTypeEnum.Read)
        {
            return Query(expression, type).FirstOrDefault();
        }

        /// <summary>
        /// 计算个数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> expression, DataBaseWriteTypeEnum type = DataBaseWriteTypeEnum.Read)
        {
            return Query(expression, type).Count();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            return DbWriteContext.Update(entity);
        }
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="expression">更新条件</param>
        /// <param name="expression2">更新字段</param>
        /// <returns></returns>
        public int Update(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TEntity>> expression2)
        {

            var rev = DbWriteContext.Update<TEntity>(expression, expression2);

            return rev;

        }

        /// <summary>
        /// 新增（返回主键）
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Insert<TEntity>(TEntity entity)
        {
            return DbWriteContext.Insert(entity);
        }

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void BatchInsert(List<TEntity> entity)
        {
            DbWriteContext.InsertRange(entity);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IJoinQuery<T1, T2> JoinQuery<T1, T2>(Expression<Func<T1, T2, object[]>> joinInfo)
        {
            return DbContext.JoinQuery<T1, T2>(joinInfo);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int Delete<TEntity>(Expression<Func<TEntity, bool>> condition)
        {
            return DbWriteContext.Delete(condition);
        }
        #endregion

        #region SQL
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<TTEntity> SqlQueryable<TTEntity>(string sql, object param = null)
        {
            var list = DbContext.SqlQuery<TTEntity>(sql, param);
            if (list == null)
            {
                return null;
            }
            else
            {
                return list.ToList();
            }
        }

        /// <summary>
        /// 查询详情
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public TEntity SqlQuerySingle(string sql, object param = null)
        {
            var info = DbContext.SqlQuery<TEntity>(sql, param).FirstOrDefault();
            return info;
        }

        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="sqlCount"></param>
        /// <param name="page"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public Result<TTEntity> SqlQueryable<TTEntity>(string sql, string sqlCount, PageEntity page, object param = null)
        {
            Result<TTEntity> data = new Result<TTEntity>()
            {
                PageIndex = page.PageIndex,
                PageSize = page.PageSize,
            };
            sql += string.Format(" offset {0} rows fetch next {1} rows only", (page.PageIndex - 1) * page.PageSize, page.PageSize);
            data.List = DbContext.SqlQuery<TTEntity>(sql, param).ToList();
            data.TotalCount = DbContext.SqlQuery<int>(sqlCount, param).FirstOrDefault();
            return data;
        }

        /// <summary>
        ///执行Insert、Update
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public TTEntity Execute<TTEntity>(string sql, object param = null)
        {
            return DbWriteContext.SqlQuery<TTEntity>(sql, param).FirstOrDefault();
        }
        #endregion


    }


    public enum DataBaseWriteTypeEnum
    {
        /// <summary>
        /// 写库
        /// </summary>
        Write = 1,
        /// <summary>
        /// 读库
        /// </summary>
        Read = 2
    }

    public enum DataBaseTypeEnum
    {
        SqlServer,
        MySql,
        Oracle,
        DB2
    }

    public enum DB
    {
        Work
    }
}
