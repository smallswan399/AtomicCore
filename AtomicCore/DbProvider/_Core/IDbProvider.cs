using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AtomicCore.DbProvider
{
    /// <summary>
    /// DB数据源操作提供接口定义
    /// </summary>
    /// <typeparam name="M"></typeparam>
    public interface IDbProvider<M>
        where M : IDbModel, new()
    {
        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="model">需要新增的数据实体</param>
        /// <returns></returns>
        DbSingleRecord<M> Insert(M model);

        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="modelList">需要新增的数据实体</param>
        /// <returns></returns>
        DbCollectionRecord<M> InsertBatch(IEnumerable<M> modelList);

        /// <summary>
        /// 更新操作（局部更新）
        /// </summary>
        /// <param name="whereExp">需要被更新的条件</param>
        /// <param name="updatePropertys">需要被替换或更新的属性</param>
        /// <returns></returns>
        DbNonRecord Update(Expression<Func<M, bool>> whereExp, Expression<Func<M, M>> updatePropertys);

        /// <summary>
        /// 更新操作（整体更新）
        /// </summary>
        /// <param name="whereExp">需要被更新的条件</param>
        /// <param name="model">需要被整体替换的实体</param>
        /// <returns></returns>
        DbNonRecord Update(Expression<Func<M, bool>> whereExp, M model);

        /// <summary>
        /// 批量自我更新
        /// </summary>
        /// <param name="modelList">所有模型都根据主键进行自我更新</param>
        /// <returns></returns>
        DbNonRecord UpdateBatch(IEnumerable<M> modelList);

        /// <summary>
        /// 批量更新任务（在一个conn.open里执行多个更新,避免多次开关造成性能损失）
        /// </summary>
        /// <param name="taskList">任务数据</param>
        /// <param name="enableSqlTransaction">是否启动SQL事务（对于单例调用最好启用，对于外层套用事务的不需要启动）</param>
        /// <returns></returns>
        DbNonRecord UpdateTask(IEnumerable<DbUpdateTaskData<M>> taskList, bool enableSqlTransaction = false);

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="deleteExp">删除条件</param>
        /// <returns></returns>
        DbNonRecord Delete(Expression<Func<M, bool>> deleteExp);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <returns></returns>
        DbSingleRecord<M> Fetch(Expression<Func<IDbFetchQueryable<M>, IDbFetchQueryable<M>>> exp);

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <returns></returns>
        DbCollectionRecord<M> FetchList(Expression<Func<IDbFetchListQueryable<M>, IDbFetchListQueryable<M>>> exp);

        /// <summary>
        /// 执行计算 Count, SUM，MAX,MIN等
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <returns></returns>
        DbCalculateRecord Calculate(Expression<Func<IDbCalculateQueryable<M>, IDbCalculateQueryable<M>>> exp);
    }
}
