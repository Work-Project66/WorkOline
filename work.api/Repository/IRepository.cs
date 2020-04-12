using Chloe;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace work.api.Repository
{
    public interface IRepository<TEntity> 
    {
        #region 对象

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        IQuery<TEntity> Query(DataBaseWriteTypeEnum type = DataBaseWriteTypeEnum.Read);

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IQuery<TEntity> Query(Expression<Func<TEntity, bool>> expression, DataBaseWriteTypeEnum type = DataBaseWriteTypeEnum.Read);

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        TEntity FindByPrimaryKey(object primaryKey);

        /// <summary>
        /// 查询列表中第一个条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, DataBaseWriteTypeEnum type = DataBaseWriteTypeEnum.Read);

        /// <summary>
        /// 计算个数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        int Count(Expression<Func<TEntity, bool>> expression, DataBaseWriteTypeEnum type = DataBaseWriteTypeEnum.Read);

        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <returns></returns>
        int Update(TEntity entity);
        /// <summary>
        /// 更新指定列
        /// </summary>
        /// <param name="expression">更新条件</param>
        /// <param name="expression2">更新字段</param>
        /// <returns></returns>
        int Update(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TEntity>> expression2);
        /// <summary>
        /// 新增（返回主键）
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Insert<TEntity>(TEntity entity);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        void BatchInsert(List<TEntity> entity);
        #endregion

        #region SQL
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        List<TTEntity> SqlQueryable<TTEntity>(string sql, object param)  ;
        /// <summary>
        /// 查询详情
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        TEntity SqlQuerySingle(string sql, object param = null);
        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="sqlCount"></param>
        /// <param name="page"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Result<TTEntity> SqlQueryable<TTEntity>(string sql, string sqlCount, PageEntity page, object param = null) ;
        /// <summary>
        ///执行Insert、Update
        /// </summary>
        /// <typeparam name="TTEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        TTEntity Execute<TTEntity>(string sql, object param = null);
        #endregion
    }
}
