using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using AtomicCore.DbProvider;

namespace AtomicCore.Integration.MssqlDbProvider
{
    /// <summary>
    /// Sql2008版本的或以后的版本的数据仓储驱动类
    /// </summary>
    /// <typeparam name="M"></typeparam>
    public class Mssql2008DbProvider<M> : IDbProvider<M>, IDbConnectionString, IDbConnectionString<M>
        where M : IDbModel, new()
    {
        #region Constructors

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private string _dbConnQuery = null;

        /// <summary>
        /// 数据库链接字符串 
        /// </summary>
        private IDbConnectionString _dbConnectionStringHandler = null;

        /// <summary>
        /// 数据库字段映射处理接口
        /// </summary>
        private IDbMappingHandler _dbMappingHandler = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnQuery">数据库链接字符串</param>
        /// <param name="dbMappingHandler">数据库字段映射处理接口</param>
        public Mssql2008DbProvider(string dbConnQuery, IDbMappingHandler dbMappingHandler)
        {
            if (string.IsNullOrEmpty(dbConnQuery))
                throw new ArgumentNullException("dbConnQuery");
            if (null == dbMappingHandler)
                throw new ArgumentNullException("dbMappingHandler");

            this._dbConnQuery = dbConnQuery;
            this._dbConnectionStringHandler = this;
            this._dbMappingHandler = dbMappingHandler;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnString">数据库连接字符串接口</param>
        /// <param name="dbMappingHandler">数据库字段映射处理接口</param>
        public Mssql2008DbProvider(IDbConnectionString dbConnString, IDbMappingHandler dbMappingHandler)
        {
            if (null == dbConnString)
                throw new ArgumentNullException("dbConnString");
            if (null == dbMappingHandler)
                throw new ArgumentNullException("dbMappingHandler");

            this._dbConnectionStringHandler = dbConnString;
            this._dbMappingHandler = dbMappingHandler;
        }

        #endregion

        #region IDBRepository<M>

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DbSingleRecord<M> Insert(M model)
        {
            DbSingleRecord<M> result = new DbSingleRecord<M>();

            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");
            Type modelT = typeof(M);

            if (model != null)
            {
                #region 判断主键数量

                DbColumnAttribute[] columns = this._dbMappingHandler.GetDbColumnCollection(modelT);
                if (null == columns || columns.Length <= 0)
                {
                    result.AppendError(string.Format("{0}类型无字段映射", modelT.FullName));
                    return result;
                }

                DbColumnAttribute[] setPrimaryKeys = columns.Where(d => d.IsDbGenerated).ToArray();
                if (setPrimaryKeys.Count() > 1)
                {
                    result.AppendError("暂不允许使用双主键！请设置一列为自增长主键！");
                    return result;
                }

                #endregion

                //需要设置参数插入的字段
                DbColumnAttribute[] setFields = columns.Where(d => !d.IsDbGenerated).ToArray();
                if (setFields.Count() > 0)
                {
                    List<DbParameter> parameters = new List<DbParameter>();

                    #region 拼接Sql语句

                    string tableName = this._dbMappingHandler.GetDbTableName(modelT);

                    StringBuilder sqlBuilder = new StringBuilder("insert into ");
                    sqlBuilder.Append("[");
                    sqlBuilder.Append(tableName);
                    sqlBuilder.Append("]");
                    sqlBuilder.Append(" (");
                    foreach (var item in setFields.Select(d => d.DbColumnName))
                    {
                        sqlBuilder.Append("[");
                        sqlBuilder.Append(item);
                        sqlBuilder.Append("],");
                    }
                    sqlBuilder.Replace(",", ")", sqlBuilder.Length - 1, 1);
                    sqlBuilder.Append(" values ");
                    sqlBuilder.Append("(");
                    foreach (var item in setFields)
                    {
                        string parameterName = string.Format("{0}", item.DbColumnName);
                        PropertyInfo p_info = this._dbMappingHandler.GetPropertySingle(modelT, item.DbColumnName);
                        object parameterValue = p_info.GetValue(model, null);

                        System.Data.SqlDbType dbType = this.GetDbtype(item.DbType);

                        DbParameter paremter = new SqlParameter(MssqlGrammarRule.GenerateParamName(parameterName), dbType);
                        paremter.Value = parameterValue;
                        parameters.Add(paremter);

                        sqlBuilder.Append(MssqlGrammarRule.GenerateParamName(parameterName));
                        sqlBuilder.Append(",");
                    }
                    sqlBuilder.Remove(sqlBuilder.Length - 1, 1);
                    sqlBuilder.Append(");");
                    sqlBuilder.Append("select SCOPE_IDENTITY();");
                    //初始化Debug
                    result.DebugInit(sqlBuilder, MssqlGrammarRule.C_ParamChar, parameters.ToArray());

                    #endregion

                    #region 执行Sql语句

                    using (DbConnection connection = new SqlConnection(dbString))
                    {
                        using (DbCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = sqlBuilder.ToString();
                            foreach (DbParameter item in parameters)
                            {
                                command.Parameters.Add(item);
                            }
                            //尝试打开数据库连结
                            if (this.TryOpenDbConnection<DbSingleRecord<M>>(connection, ref result))
                            {
                                if (setPrimaryKeys != null && setPrimaryKeys.Count() > 0)
                                {
                                    try
                                    {
                                        object dbVal = command.ExecuteScalar();
                                        PropertyInfo pinfo = this._dbMappingHandler.GetPropertySingle(modelT, setPrimaryKeys.First().DbColumnName);
                                        if (pinfo != null && dbVal != DBNull.Value)
                                        {
                                            dbVal = Convert.ChangeType(dbVal, pinfo.PropertyType);
                                            pinfo.SetValue(model, dbVal, null);
                                        }
                                        result.Record = model;
                                    }
                                    catch (Exception ex)
                                    {
                                        result.Record = default(M);
                                        result.AppendException(ex);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        int affectedRow = command.ExecuteNonQuery();
                                        if (affectedRow > 0)
                                        {
                                            result.Record = model;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        result.Record = default(M);
                                        result.AppendException(ex);
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    return result;
                }
                else
                {
                    result.AppendError("插入的表仅有自增长列或没有指定任何列");
                    return result;
                }
            }
            else
            {
                result.AppendError("插入数据时候的Model为空");
                return result;
            }
        }

        /// <summary>
        /// 批露插入数据(返回的集合若存在自增长主键,均未赋值)
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public DbCollectionRecord<M> InsertBatch(IEnumerable<M> modelList)
        {
            DbCollectionRecord<M> result = new DbCollectionRecord<M>();

            if (null == modelList || !modelList.Any())
                return result;

            //获取Db数据库链接字符串
            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");

            //获取当前模型的类型和DB类型
            Type modelT = typeof(M);
            DbColumnAttribute[] columns = this._dbMappingHandler.GetDbColumnCollection(modelT);

            //构造内存数据表
            DataTable dt = new DataTable();
            dt.Columns.AddRange(this._dbMappingHandler.GetPropertyCollection(modelT).Select(s => new DataColumn(this._dbMappingHandler.GetDbColumnSingle(modelT, s.Name).DbColumnName, s.PropertyType)).ToArray());

            //开始向虚拟内存表中进行映射
            foreach (var item in modelList)
            {
                DataRow r = dt.NewRow();

                foreach (var col in columns)
                    r[col.DbColumnName] = this._dbMappingHandler.GetPropertySingle(modelT, col.DbColumnName).GetValue(item, null);

                dt.Rows.Add(r);
            }

            //开始执行
            using (SqlConnection connection = new SqlConnection(dbString))
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = this._dbMappingHandler.GetDbTableName(modelT);
                    bulkCopy.BatchSize = dt.Rows.Count;

                    //尝试打开数据库连结
                    if (this.TryOpenDbConnection<DbCollectionRecord<M>>(connection, ref result))
                    {
                        bulkCopy.WriteToServer(dt);

                        result.Record = new List<M>(modelList);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 更新操作（局部更新）
        /// </summary>
        /// <param name="whereExp">需要被更新的条件</param>
        /// <param name="updatePropertys">需要被替换或更新的属性</param>
        /// <returns></returns>
        public DbNonRecord Update(Expression<Func<M, bool>> whereExp, Expression<Func<M, M>> updatePropertys)
        {
            DbNonRecord result = new DbNonRecord();

            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");

            Type modelT = typeof(M);
            Mssql2008WhereScriptResult whereResult = null;
            Mssql2008UpdateScriptResult updatePropertyResult = null;

            #region 解析where条件

            //允许null，即不设置任何条件
            if (whereExp != null)
            {
                Expression where_func_lambdaExp = null;
                if (whereExp is LambdaExpression)
                {
                    //在方法参数上直接写条件
                    where_func_lambdaExp = whereExp;
                }
                else if (whereExp is MemberExpression)
                {
                    //通过条件组合的模式
                    object lambdaObject = ExpressionCalculater.GetValue(whereExp);
                    where_func_lambdaExp = lambdaObject as Expression;
                }
                else
                {
                    result.AppendError("尚未实现直接解析" + whereExp.NodeType.ToString() + "的特例");
                    return result;
                }

                //解析Where条件
                whereResult = Mssql2008WhereScriptHandler.ExecuteResolver(where_func_lambdaExp, this._dbMappingHandler, false);
                if (!whereResult.IsAvailable())
                {
                    result.CopyStatus(whereResult);
                    return result;
                }
            }

            #endregion

            #region 解析需要被更新的字段

            if (updatePropertys != null)
            {
                if (updatePropertys is LambdaExpression && updatePropertys.Body.NodeType == ExpressionType.MemberInit)
                {
                    updatePropertyResult = Mssql2008UpdateScriptHandler.ExecuteResolver(updatePropertys, this._dbMappingHandler);
                    if (!updatePropertyResult.IsAvailable())
                    {
                        result.CopyStatus(updatePropertyResult);
                        return result;
                    }
                }
                else
                {
                    result.AppendError("updatePropertys表达式格式异常,表达式格式必须是MemberInit,例如：d => new News() { Content = d.Content + \":已变更\" }");
                    return result;
                }
            }
            else
            {
                result.AppendError("updatePropertys不允许为null,至少指定一个需要被修改的列");
                return result;
            }

            #endregion

            #region 开始拼装Sql语句

            //获取所有的数据源列
            DbColumnAttribute[] colums = this._dbMappingHandler.GetDbColumnCollection(modelT);

            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter cur_parameter = null;
            StringBuilder sqlBuilder = new StringBuilder("update ");
            sqlBuilder.Append("[");
            sqlBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
            sqlBuilder.Append("]");
            sqlBuilder.Append(" set ");
            foreach (var item in updatePropertyResult.FieldMembers)
            {
                //自增长的自动跳过
                if (colums.Any(d => d.PropertyNameMapping == item.PropertyItem.Name && d.IsDbGenerated))
                {
                    continue;
                }

                string cur_field = colums.First(d => d.PropertyNameMapping == item.PropertyItem.Name).DbColumnName;

                sqlBuilder.Append(" ");
                sqlBuilder.Append("[");
                sqlBuilder.Append(cur_field);
                sqlBuilder.Append("]");
                sqlBuilder.Append("=");
                sqlBuilder.Append(item.UpdateTextFragment);
                sqlBuilder.Append(",");

                foreach (var pitem in item.Parameter)
                {
                    cur_parameter = new SqlParameter(pitem.Name, pitem.Value);
                    parameters.Add(cur_parameter);
                }
            }
            sqlBuilder.Replace(",", " ", sqlBuilder.Length - 1, 1);
            if (whereResult != null)
            {
                sqlBuilder.Append("where ");
                sqlBuilder.Append(whereResult.TextScript);
                foreach (var item in whereResult.Parameters)
                {
                    cur_parameter = new SqlParameter(item.Name, item.Value);
                    parameters.Add(cur_parameter);
                }
            }
            sqlBuilder.Append(";");
            //初始化Debug
            result.DebugInit(sqlBuilder, MssqlGrammarRule.C_ParamChar, parameters.ToArray());

            #endregion

            #region 执行Sql语句

            using (DbConnection connection = new SqlConnection(dbString))
            {
                using (DbCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sqlBuilder.ToString();
                    foreach (DbParameter item in parameters)
                    {
                        command.Parameters.Add(item);
                    }
                    //尝试打开数据库连结
                    if (this.TryOpenDbConnection<DbNonRecord>(connection, ref result))
                    {
                        try
                        {
                            result.AffectedRow = command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            result.AppendError("sql语句执行异常," + command.CommandText);
                            result.AppendException(ex);
                        }
                    }
                }
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 更新操作（整体更新）
        /// </summary>
        /// <param name="whereExp">需要被更新的条件</param>
        /// <param name="model">需要被整体替换的实体</param>
        /// <returns></returns>
        public DbNonRecord Update(Expression<Func<M, bool>> whereExp, M model)
        {
            DbNonRecord result = new DbNonRecord();

            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");

            Type modelT = typeof(M);
            Mssql2008WhereScriptResult whereResult = null;

            #region 验证需要被修改的实体是否为null

            if (model == null)
            {
                result.AppendError("修改数据时候的Model为空");
                return result;
            }

            #endregion

            #region 解析where条件

            //允许null，即不设置任何条件
            if (whereExp != null)
            {
                Expression where_func_lambdaExp = null;
                if (whereExp is LambdaExpression)
                {
                    //在方法参数上直接写条件
                    where_func_lambdaExp = whereExp;
                }
                else if (whereExp is MemberExpression)
                {
                    //通过条件组合的模式
                    object lambdaObject = ExpressionCalculater.GetValue(whereExp);
                    where_func_lambdaExp = lambdaObject as Expression;
                }
                else
                {
                    result.AppendError("尚未实现直接解析" + whereExp.NodeType.ToString() + "的特例");
                    return result;
                }

                //执行where解析
                whereResult = Mssql2008WhereScriptHandler.ExecuteResolver(where_func_lambdaExp, this._dbMappingHandler, false);
                if (!whereResult.IsAvailable())
                {
                    result.CopyStatus(whereResult);
                    return result;
                }
            }

            #endregion

            #region 开始拼接Sql语句

            DbColumnAttribute[] columns = this._dbMappingHandler.GetDbColumnCollection(modelT, d => !d.IsDbGenerated);

            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter cur_parameter = null;
            StringBuilder sqlBuilder = new StringBuilder("update ");
            sqlBuilder.Append("[");
            sqlBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
            sqlBuilder.Append("]");
            sqlBuilder.Append(" set ");
            foreach (var item in columns)
            {
                PropertyInfo p = modelT.GetProperty(item.PropertyNameMapping);
                if (p != null)
                {
                    string parameterName = string.Format("set_{0}", item.DbColumnName);
                    object parameterVal = p.GetValue(model, null);

                    sqlBuilder.Append("[");
                    sqlBuilder.Append(item.DbColumnName);
                    sqlBuilder.Append("]");
                    sqlBuilder.Append("=");
                    sqlBuilder.Append("@");
                    sqlBuilder.Append(parameterName);
                    sqlBuilder.Append(",");

                    cur_parameter = new SqlParameter(MssqlGrammarRule.GenerateParamName(parameterName), parameterVal);
                    parameters.Add(cur_parameter);
                }
            }
            sqlBuilder.Replace(",", " ", sqlBuilder.Length - 1, 1);
            if (whereResult != null)
            {
                sqlBuilder.Append("where ");
                sqlBuilder.Append(whereResult.TextScript);
                foreach (var item in whereResult.Parameters)
                {
                    cur_parameter = new SqlParameter(item.Name, item.Value);
                    parameters.Add(cur_parameter);
                }
            }
            sqlBuilder.Append(";");
            //初始化Debug
            result.DebugInit(sqlBuilder, MssqlGrammarRule.C_ParamChar, parameters.ToArray());

            #endregion

            #region 执行Sql语句

            using (DbConnection connection = new SqlConnection(dbString))
            {
                using (DbCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sqlBuilder.ToString();
                    foreach (DbParameter item in parameters)
                    {
                        command.Parameters.Add(item);
                    }
                    //尝试打开数据库连结
                    if (this.TryOpenDbConnection<DbNonRecord>(connection, ref result))
                    {
                        try
                        {
                            result.AffectedRow = command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            result.AppendError("sql语句执行异常," + command.CommandText);
                            result.AppendException(ex);
                        }
                    }
                }
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 批量自我更新
        /// </summary>
        /// <param name="modelList">所有模型都根据主键进行自我更新</param>
        /// <returns></returns>
        public DbNonRecord UpdateBatch(IEnumerable<M> modelList)
        {
            DbNonRecord result = new DbNonRecord();

            if (null == modelList || !modelList.Any())
                return result;

            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");

            #region 定义全局参数

            Type modelT = typeof(M);
            string origTableName = this._dbMappingHandler.GetDbTableName(modelT);
            string bulkTableName = string.Format("#{0}_{1}", origTableName, Guid.NewGuid().ToString("N"));
            DbColumnAttribute[] columns = this._dbMappingHandler.GetDbColumnCollection(modelT);

            #endregion

            #region 生成数据库中的临时表(仅复制表结构)

            StringBuilder sqlBulkTableBuilder = new StringBuilder();
            sqlBulkTableBuilder.AppendFormat("select * into {0} from {1} where 1 = 0;", bulkTableName, origTableName);

            DataTable bulkDT = new DataTable();
            bulkDT.Columns.AddRange(this._dbMappingHandler.GetPropertyCollection(modelT).Select(s => new DataColumn(this._dbMappingHandler.GetDbColumnSingle(modelT, s.Name).DbColumnName, s.PropertyType)).ToArray());

            foreach (var item in modelList)
            {
                DataRow r = bulkDT.NewRow();

                foreach (var col in columns.Where(d => !d.IsDbGenerated))
                    r[col.DbColumnName] = this._dbMappingHandler.GetPropertySingle(modelT, col.DbColumnName).GetValue(item, null);

                bulkDT.Rows.Add(r);
            }

            #endregion

            #region 开始拼接与临时表相关的Update语句

            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("update a set ");
            foreach (var col in columns)
            {
                if (!col.IsDbPrimaryKey && !col.IsDbGenerated)
                    sqlBuilder.AppendFormat("[a].[{0}] = [b].[{0}],", col.DbColumnName);
            }
            sqlBuilder.Replace(",", " ", sqlBuilder.Length - 1, 1);
            sqlBuilder.AppendFormat("from [{0}] as a inner join [{1}] as b on ", origTableName, bulkTableName);
            foreach (var pkcol in columns.Where(d => d.IsDbPrimaryKey))
                sqlBuilder.AppendFormat("[a].[{0}] = [b].[{0}] and ", pkcol.DbColumnName);
            sqlBuilder.Remove(sqlBuilder.Length - 4, 4);
            sqlBuilder.AppendFormat(";drop table [{0}];", bulkTableName);

            #endregion

            #region 执行Sql语句

            using (SqlConnection connection = new SqlConnection(dbString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    //设置command连接字符串
                    command.Connection = connection;

                    //尝试打开数据库连结
                    if (this.TryOpenDbConnection<DbNonRecord>(connection, ref result))
                    {
                        //1.创建局部临时表
                        command.CommandText = sqlBulkTableBuilder.ToString();
                        command.ExecuteNonQuery();

                        //2.批量插入临时表
                        using (SqlBulkCopy bulkcopy = new SqlBulkCopy(connection))
                        {
                            bulkcopy.DestinationTableName = bulkTableName;
                            bulkcopy.BatchSize = bulkDT.Rows.Count;
                            bulkcopy.WriteToServer(bulkDT);
                        }

                        //3.执行SQL本体语句
                        try
                        {
                            command.CommandText = sqlBuilder.ToString();
                            result.AffectedRow = command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            result.AppendError("sql语句执行异常," + command.CommandText);
                            result.AppendException(ex);
                        }
                    }
                }
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 批量更新任务（在一个conn.open里执行多个更新,避免多次开关造成性能损失）
        /// </summary>
        /// <param name="taskList">任务数据</param>
        /// <param name="enableSqlTransaction">是否启动SQL事务（对于单例调用最好启用，对于外层套用事务的不需要启动）</param>
        /// <returns></returns>
        public DbNonRecord UpdateTask(IEnumerable<DbUpdateTaskData<M>> taskList, bool enableSqlTransaction = false)
        {
            DbNonRecord result = new DbNonRecord();

            if (null == taskList || !taskList.Any())
                return result;

            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");

            #region 定义全局参数

            Type modelT = typeof(M);
            string origTableName = this._dbMappingHandler.GetDbTableName(modelT);
            DbColumnAttribute[] columns = this._dbMappingHandler.GetDbColumnCollection(modelT, d => !d.IsDbGenerated);

            #endregion

            #region 开始循环解析任务

            List<DbUpdateTaskSqlData> sqlDataList = new List<DbUpdateTaskSqlData>();

            foreach (var task in taskList)
            {
                #region 跳过循环特殊条件

                if (null == task.WhereExp || null == task.UpdatePropertys)
                    continue;

                #endregion

                #region 条件解析

                Expression where_func_lambdaExp = null;
                if (task.WhereExp is LambdaExpression)
                {
                    //在方法参数上直接写条件
                    where_func_lambdaExp = task.WhereExp;
                }
                else if (task.WhereExp is MemberExpression)
                {
                    //通过条件组合的模式
                    object lambdaObject = ExpressionCalculater.GetValue(task.WhereExp);
                    where_func_lambdaExp = lambdaObject as Expression;
                }
                else
                {
                    result.AppendError("尚未实现直接解析" + task.WhereExp.NodeType.ToString() + "的特例");
                    return result;
                }

                //解析Where条件
                Mssql2008WhereScriptResult whereResult = Mssql2008WhereScriptHandler.ExecuteResolver(where_func_lambdaExp, this._dbMappingHandler, false);
                if (!whereResult.IsAvailable())
                {
                    result.CopyStatus(whereResult);
                    return result;
                }

                #endregion

                #region 更新字段解析

                Mssql2008UpdateScriptResult updatePropertyResult = null;
                if (task.UpdatePropertys is LambdaExpression && task.UpdatePropertys.Body.NodeType == ExpressionType.MemberInit)
                {
                    updatePropertyResult = Mssql2008UpdateScriptHandler.ExecuteResolver(task.UpdatePropertys, this._dbMappingHandler);
                    if (!updatePropertyResult.IsAvailable())
                    {
                        result.CopyStatus(updatePropertyResult);
                        return result;
                    }
                }
                else
                {
                    result.AppendError("updatePropertys表达式格式异常,表达式格式必须是MemberInit,例如：d => new News() { Content = d.Content + \":已变更\" }");
                    return result;
                }

                #endregion

                #region 开始拼装Sql语句

                //获取所有的数据源列
                DbColumnAttribute[] colums = this._dbMappingHandler.GetDbColumnCollection(modelT);

                List<DbParameter> parameters = new List<DbParameter>();
                DbParameter cur_parameter = null;
                StringBuilder sqlBuilder = new StringBuilder("update ");
                sqlBuilder.Append("[");
                sqlBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                sqlBuilder.Append("]");
                sqlBuilder.Append(" set ");
                foreach (var item in updatePropertyResult.FieldMembers)
                {
                    //自增长的自动跳过
                    if (colums.Any(d => d.PropertyNameMapping == item.PropertyItem.Name && d.IsDbGenerated))
                        continue;

                    string cur_field = colums.First(d => d.PropertyNameMapping == item.PropertyItem.Name).DbColumnName;

                    sqlBuilder.Append(" ");
                    sqlBuilder.Append("[");
                    sqlBuilder.Append(cur_field);
                    sqlBuilder.Append("]");
                    sqlBuilder.Append("=");
                    sqlBuilder.Append(item.UpdateTextFragment);
                    sqlBuilder.Append(",");

                    foreach (var pitem in item.Parameter)
                    {
                        cur_parameter = new SqlParameter(pitem.Name, pitem.Value);
                        parameters.Add(cur_parameter);
                    }
                }
                sqlBuilder.Replace(",", " ", sqlBuilder.Length - 1, 1);
                if (whereResult != null)
                {
                    sqlBuilder.Append("where ");
                    sqlBuilder.Append(whereResult.TextScript);
                    foreach (var item in whereResult.Parameters)
                    {
                        cur_parameter = new SqlParameter(item.Name, item.Value);
                        parameters.Add(cur_parameter);
                    }
                }
                sqlBuilder.Append(";");

                #endregion

                #region 填充SqlDataList

                sqlDataList.Add(new DbUpdateTaskSqlData()
                {
                    SqlText = sqlBuilder.ToString(),
                    SqlParameters = parameters.ToArray()
                });

                #endregion
            }

            #endregion

            #region 执行Sql语句

            using (SqlConnection connection = new SqlConnection(dbString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;

                    //尝试打开数据库连结
                    if (this.TryOpenDbConnection<DbNonRecord>(connection, ref result))
                    {
                        //判断是否需要开启事务
                        command.Transaction = enableSqlTransaction ? connection.BeginTransaction(IsolationLevel.ReadCommitted) : null;

                        //开始循环执行Sql
                        foreach (var sql in sqlDataList)
                        {
                            command.CommandText = sql.SqlText;
                            command.Parameters.Clear();
                            command.Parameters.AddRange(sql.SqlParameters);

                            try
                            {
                                result.AffectedRow += command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                result.AppendError("sql语句执行异常," + command.CommandText);
                                result.AppendException(ex);

                                if (null != command.Transaction)
                                    command.Transaction.Rollback();
                            }
                        }

                        if (null != command.Transaction)
                            command.Transaction.Commit();
                    }
                }
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="deleteExp">删除条件</param>
        /// <returns></returns>
        public DbNonRecord Delete(Expression<Func<M, bool>> deleteExp)
        {
            DbNonRecord result = new DbNonRecord();

            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");

            Type modelT = typeof(M);
            Mssql2008WhereScriptResult whereResult = null;
            if (deleteExp != null)
            {
                #region 解析条件语句

                Expression where_func_lambdaExp = null;
                if (deleteExp is LambdaExpression)
                {
                    //在方法参数上直接写条件
                    where_func_lambdaExp = deleteExp;
                }
                else if (deleteExp is MemberExpression)
                {
                    //通过条件组合的模式
                    object lambdaObject = ExpressionCalculater.GetValue(deleteExp);
                    where_func_lambdaExp = lambdaObject as Expression;
                }
                else
                {
                    result.AppendError("尚未实现直接解析" + deleteExp.NodeType.ToString() + "的特例");
                    return result;
                }

                //执行where解析
                whereResult = Mssql2008WhereScriptHandler.ExecuteResolver(where_func_lambdaExp, this._dbMappingHandler, false);
                if (!whereResult.IsAvailable())
                {
                    result.CopyStatus(whereResult);
                    return result;
                }

                #endregion

                #region 拼接Sql语句

                List<DbParameter> parameters = new List<DbParameter>();
                DbParameter cur_parameter = null;
                StringBuilder sqlBuilder = new StringBuilder("delete from ");
                sqlBuilder.Append("[");
                sqlBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                sqlBuilder.Append("]");
                sqlBuilder.Append(" where ");
                sqlBuilder.Append(whereResult.TextScript);
                foreach (var item in whereResult.Parameters)
                {
                    cur_parameter = new SqlParameter(item.Name, item.Value);
                    parameters.Add(cur_parameter);
                }
                sqlBuilder.Append(";");
                //初始化Debug
                result.DebugInit(sqlBuilder, MssqlGrammarRule.C_ParamChar, parameters.ToArray());

                #endregion

                #region 开始执行Sql语句

                using (DbConnection connection = new SqlConnection(dbString))
                {
                    using (DbCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = sqlBuilder.ToString();
                        foreach (DbParameter item in parameters)
                        {
                            command.Parameters.Add(item);
                        }
                        //尝试打开数据库连结
                        if (this.TryOpenDbConnection<DbNonRecord>(connection, ref result))
                        {
                            try
                            {
                                result.AffectedRow = command.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                result.AppendError("sql语句执行异常," + command.CommandText);
                                result.AppendException(ex);
                            }
                        }
                    }
                }

                #endregion

                return result;
            }
            else
            {
                result.AppendError("不允许传入null条件进行删除，此行为属于非法行为！");
                return result;
            }
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <returns></returns>
        public DbSingleRecord<M> Fetch(Expression<Func<IDbFetchQueryable<M>, IDbFetchQueryable<M>>> exp)
        {
            DbSingleRecord<M> result = new DbSingleRecord<M>();

            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");

            Type modelT = typeof(M);
            Mssql2008SentenceResult resolveResult = null;
            List<DbParameter> parameters = new List<DbParameter>();

            #region 解析表达式条件

            if (exp != null)
            {
                resolveResult = Mssql2008SentenceHandler.ExecuteResolver(exp, this._dbMappingHandler);
                if (!resolveResult.IsAvailable())
                {
                    result.CopyStatus(resolveResult);
                    return result;
                }
            }

            #endregion

            #region 拼接SQL语句

            StringBuilder sqlBuilder = new StringBuilder("select top 1 ");
            if (resolveResult == null)
            {
                sqlBuilder.Append(" * from ");
                sqlBuilder.Append("[");
                sqlBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                sqlBuilder.Append("]");
            }
            else
            {
                #region 指定需要查询的字段

                if (resolveResult.SqlSelectFields == null || resolveResult.SqlSelectFields.Count() <= 0)
                {
                    //如果没有设置要查询的字段，默认查询所有
                    DbColumnAttribute[] fields = this._dbMappingHandler.GetDbColumnCollection(modelT);

                    MssqlSelectField fieldItem = null;
                    foreach (var item in fields)
                    {
                        fieldItem = new MssqlSelectField();
                        fieldItem.DBFieldAsName = item.DbColumnName;
                        fieldItem.DBSelectFragment = item.DbColumnName;
                        fieldItem.IsModelProperty = true;
                        resolveResult.SetSelectField(fieldItem);
                    }
                }
                foreach (var item in resolveResult.SqlSelectFields)
                {
                    if (item.IsModelProperty)
                    {
                        sqlBuilder.Append("[");
                        sqlBuilder.Append(item.DBSelectFragment);
                        sqlBuilder.Append("]");
                        sqlBuilder.Append(" as ");
                        sqlBuilder.Append("[");
                        sqlBuilder.Append(item.DBFieldAsName);
                        sqlBuilder.Append("]");
                        sqlBuilder.Append(",");
                    }
                }
                sqlBuilder.Replace(",", "", sqlBuilder.Length - 1, 1);
                sqlBuilder.Append(" from ");
                sqlBuilder.Append("[");
                sqlBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                sqlBuilder.Append("] ");

                #endregion

                #region 指定Where条件

                if (!string.IsNullOrEmpty(resolveResult.SqlWhereConditionText))
                {
                    sqlBuilder.Append(" where ");
                    sqlBuilder.Append(resolveResult.SqlWhereConditionText);
                }
                //装载参数
                if (resolveResult.SqlQuerylParameters != null && resolveResult.SqlQuerylParameters.Count() > 0)
                {
                    DbParameter cur_parameter = null;
                    foreach (var item in resolveResult.SqlQuerylParameters)
                    {
                        cur_parameter = new SqlParameter(item.Name, item.Value);
                        parameters.Add(cur_parameter);
                    }
                }

                #endregion

                #region 指定Order条件

                if (!string.IsNullOrEmpty(resolveResult.SqlOrderConditionText))
                {
                    sqlBuilder.Append(" order by ");
                    sqlBuilder.Append(resolveResult.SqlOrderConditionText);
                }

                #endregion
            }
            sqlBuilder.Append(";");
            //初始化Debug
            result.DebugInit(sqlBuilder, MssqlGrammarRule.C_ParamChar, parameters.ToArray());

            #endregion

            #region 执行Sql语句

            using (DbConnection connection = new SqlConnection(dbString))
            {
                using (DbCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = sqlBuilder.ToString();
                    if (parameters.Count > 0)
                    {
                        foreach (var item in parameters)
                        {
                            command.Parameters.Add(item);
                        }
                    }
                    //尝试打开数据库连结
                    if (this.TryOpenDbConnection<DbSingleRecord<M>>(connection, ref result))
                    {
                        //尝试执行SQL语句
                        DbDataReader reader = this.TryExecuteReader<DbSingleRecord<M>>(command, ref result);
                        if (reader != null && reader.HasRows && reader.Read())
                        {
                            result.Record = this.AutoFillModel(reader, modelT, resolveResult.SqlSelectFields);
                            //释放资源，关闭连结
                            this.DisposeReader(reader);
                        }
                    }
                }
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <returns></returns>
        public DbCollectionRecord<M> FetchList(Expression<Func<IDbFetchListQueryable<M>, IDbFetchListQueryable<M>>> exp)
        {
            DbCollectionRecord<M> result = new DbCollectionRecord<M>();

            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");

            Type modelT = typeof(M);
            DbColumnAttribute[] columns = this._dbMappingHandler.GetDbColumnCollection(modelT);
            if (null == columns || columns.Length <= 0)
            {
                result.AppendError(string.Format("类型{0}未映射对上任何字段", modelT.FullName));
                return result;
            }

            Mssql2008SentenceResult resolveResult = null;
            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter cur_parameter = null;
            int currentPage = 0;
            int pageSize = 0;

            #region 解析表达式条件

            if (exp != null)
            {
                resolveResult = Mssql2008SentenceHandler.ExecuteResolver(exp, this._dbMappingHandler);
                if (!resolveResult.IsAvailable())
                {
                    result.CopyStatus(resolveResult);
                    return result;
                }
            }

            #endregion

            #region 拼接SQL语句

            StringBuilder countBuilder = new StringBuilder();
            StringBuilder queryBuilder = new StringBuilder("select ");

            if (resolveResult == null)
            {
                currentPage = Mssql2008SentenceResult.DEFAULT_CURRENTPAGE;
                pageSize = Mssql2008SentenceResult.DEFAULT_PAGESIZE;

                #region 拼接构造统计语句

                countBuilder.Append("select count(1) from ");
                countBuilder.Append("[");
                countBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                countBuilder.Append("]");

                #endregion

                #region 拼接构造查询语句

                queryBuilder.Append(" top ");
                queryBuilder.Append(pageSize);
                queryBuilder.Append(" * from ");
                queryBuilder.Append("[");
                queryBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                queryBuilder.Append("]");

                #endregion
            }
            else
            {
                currentPage = resolveResult.SqlPagerCondition.Key;
                pageSize = resolveResult.SqlPagerCondition.Value;

                #region 拼接构造统计语句

                countBuilder.Append("select count(1) from ");
                countBuilder.Append("[");
                countBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                countBuilder.Append("] ");
                if (!string.IsNullOrEmpty(resolveResult.SqlWhereConditionText))
                {
                    countBuilder.Append(" where ");
                    countBuilder.Append(resolveResult.SqlWhereConditionText);
                }

                #endregion

                #region 拼接构造查询语句

                //检查翻页参数
                if (currentPage < 1)
                {
                    currentPage = Mssql2008SentenceResult.DEFAULT_CURRENTPAGE;
                }
                if (pageSize < 1)
                {
                    pageSize = Mssql2008SentenceResult.DEFAULT_PAGESIZE;
                }

                //第一页起始数据
                if (currentPage == 1)
                {
                    #region 设置头N条数据

                    queryBuilder.Append(" top ");
                    queryBuilder.Append(pageSize);
                    queryBuilder.Append(" ");

                    #endregion

                    #region 指定需要查询的字段

                    if (resolveResult.SqlSelectFields == null || resolveResult.SqlSelectFields.Count() <= 0)
                    {
                        //如果没有设置要查询的字段，默认查询所有
                        MssqlSelectField fieldItem = null;
                        foreach (var item in columns)
                        {
                            fieldItem = new MssqlSelectField();
                            fieldItem.DBFieldAsName = item.DbColumnName;
                            fieldItem.DBSelectFragment = item.DbColumnName;
                            fieldItem.IsModelProperty = true;
                            resolveResult.SetSelectField(fieldItem);
                        }
                    }
                    foreach (var item in resolveResult.SqlSelectFields)
                    {
                        if (item.IsModelProperty)
                        {
                            queryBuilder.Append("[");
                            queryBuilder.Append(item.DBSelectFragment);
                            queryBuilder.Append("]");
                            queryBuilder.Append(" as ");
                            queryBuilder.Append("[");
                            queryBuilder.Append(item.DBFieldAsName);
                            queryBuilder.Append("]");
                            queryBuilder.Append(",");
                        }
                    }
                    queryBuilder.Replace(",", "", queryBuilder.Length - 1, 1);
                    queryBuilder.Append(" from ");
                    queryBuilder.Append("[");
                    queryBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                    queryBuilder.Append("] ");

                    #endregion

                    #region 指定Where条件

                    if (!string.IsNullOrEmpty(resolveResult.SqlWhereConditionText))
                    {
                        queryBuilder.Append(" where ");
                        queryBuilder.Append(resolveResult.SqlWhereConditionText);
                    }
                    if (resolveResult.SqlQuerylParameters != null && resolveResult.SqlQuerylParameters.Count() > 0)
                    {
                        foreach (var item in resolveResult.SqlQuerylParameters)
                        {
                            cur_parameter = new SqlParameter(item.Name, item.Value);
                            parameters.Add(cur_parameter);
                        }
                    }

                    #endregion

                    #region 指定Order条件

                    if (!string.IsNullOrEmpty(resolveResult.SqlOrderConditionText))
                    {
                        queryBuilder.Append(" order by ");
                        queryBuilder.Append(resolveResult.SqlOrderConditionText);
                    }

                    #endregion
                }
                //第N页数据
                else
                {
                    queryBuilder.Append(" * from (");
                    queryBuilder.Append("select row_number() over (order by ");

                    #region 指定排序

                    if (string.IsNullOrEmpty(resolveResult.SqlOrderConditionText))
                    {
                        //设置主键倒序排序
                        IEnumerable<string> pks = columns.Where(d => d.IsDbPrimaryKey).Select(d => d.DbColumnName);
                        foreach (var item in pks)
                        {
                            queryBuilder.Append("[");
                            queryBuilder.Append(item);
                            queryBuilder.Append("]");
                            queryBuilder.Append(" desc,");
                        }
                        queryBuilder.Replace(",", "", queryBuilder.Length - 1, 1);
                    }
                    else
                    {
                        queryBuilder.Append(resolveResult.SqlOrderConditionText);
                    }
                    queryBuilder.Append(") as [RowId],");

                    #endregion

                    #region 指定查询的字段

                    if (resolveResult.SqlSelectFields == null || resolveResult.SqlSelectFields.Count() <= 0)
                    {
                        //如果没有设置要查询的字段，默认查询所有
                        MssqlSelectField fieldItem = null;
                        foreach (var item in columns)
                        {
                            fieldItem = new MssqlSelectField();
                            fieldItem.DBFieldAsName = item.DbColumnName;
                            fieldItem.DBSelectFragment = item.DbColumnName;
                            fieldItem.IsModelProperty = true;
                            resolveResult.SetSelectField(fieldItem);
                        }
                    }
                    foreach (var item in resolveResult.SqlSelectFields)
                    {
                        if (item.IsModelProperty)
                        {
                            queryBuilder.Append("[");
                            queryBuilder.Append(item.DBSelectFragment);
                            queryBuilder.Append("]");
                            queryBuilder.Append(" as ");
                            queryBuilder.Append("[");
                            queryBuilder.Append(item.DBFieldAsName);
                            queryBuilder.Append("]");
                            queryBuilder.Append(",");
                        }
                    }
                    queryBuilder.Replace(",", "", queryBuilder.Length - 1, 1);

                    #endregion

                    #region 指定查询的表

                    queryBuilder.Append(" from ");
                    queryBuilder.Append("[");
                    queryBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                    queryBuilder.Append("]");

                    #endregion

                    #region 指定Where条件

                    if (!string.IsNullOrEmpty(resolveResult.SqlWhereConditionText))
                    {
                        queryBuilder.Append(" where ");
                        queryBuilder.Append(resolveResult.SqlWhereConditionText);
                    }
                    if (resolveResult.SqlQuerylParameters != null && resolveResult.SqlQuerylParameters.Count() > 0)
                    {
                        foreach (var item in resolveResult.SqlQuerylParameters)
                        {
                            cur_parameter = new SqlParameter(item.Name, item.Value);
                            parameters.Add(cur_parameter);
                        }
                    }

                    #endregion

                    queryBuilder.Append(") [T1] ");
                    queryBuilder.Append(" where [RowId]>=");
                    queryBuilder.Append(((currentPage - 1) * pageSize + 1));
                    queryBuilder.Append(" and ");
                    queryBuilder.Append(" [RowId]<= ");
                    queryBuilder.Append((currentPage * pageSize));
                    queryBuilder.Append(" order by [RowId] asc");
                }

                #endregion
            }
            countBuilder.Append(";");
            queryBuilder.Append(";");
            //初始化Debug
            result.DebugInit(new StringBuilder(countBuilder.ToString() + queryBuilder.ToString()), MssqlGrammarRule.C_ParamChar, parameters.ToArray());

            #endregion

            #region 执行Sql语句

            using (DbConnection connection = new SqlConnection(dbString))
            {
                using (DbCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    if (parameters.Count > 0)
                    {
                        foreach (var item in parameters)
                        {
                            command.Parameters.Add(item);
                        }
                    }
                    //尝试打开数据库链接
                    if (this.TryOpenDbConnection<DbCollectionRecord<M>>(connection, ref result))
                    {
                        //尝试执行语句返回第一行第一列
                        command.CommandText = countBuilder.ToString();
                        result.TotalCount = Convert.ToInt32(command.ExecuteScalar());

                        //如果存在符合条件的数据则进行二次查询，否则跳出
                        if (result.TotalCount > 0)
                        {
                            result.CurrentPage = currentPage;
                            result.PageSize = pageSize;

                            //尝试执行语句返回DataReader
                            command.CommandText = queryBuilder.ToString();
                            DbDataReader reader = this.TryExecuteReader<DbCollectionRecord<M>>(command, ref result);
                            if (reader != null && reader.HasRows)
                            {
                                result.Record = new List<M>();
                                M entity = default(M);
                                while (reader.Read())
                                {
                                    entity = this.AutoFillModel(reader, modelT, resolveResult.SqlSelectFields);
                                    result.Record.Add(entity);
                                }
                                //释放资源，关闭连结
                                this.DisposeReader(reader);
                            }
                        }

                    }
                }
            }

            #endregion

            return result;
        }

        /// <summary>
        /// 执行计算 Count, SUM，MAX,MIN等
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <returns></returns>
        public DbCalculateRecord Calculate(Expression<Func<IDbCalculateQueryable<M>, IDbCalculateQueryable<M>>> exp)
        {
            DbCalculateRecord result = new DbCalculateRecord();

            string dbString = this._dbConnectionStringHandler.GetConnection();
            if (string.IsNullOrEmpty(dbString))
                throw new Exception("dbString is null");

            Type modelT = typeof(M);
            Mssql2008SentenceResult resolveResult = null;
            List<DbParameter> parameters = new List<DbParameter>();
            DbParameter cur_parameter = null;

            #region 解析表达式条件

            if (exp != null)
            {
                resolveResult = Mssql2008SentenceHandler.ExecuteResolver(exp, this._dbMappingHandler);
                if (!resolveResult.IsAvailable())
                {
                    result.CopyStatus(resolveResult);
                    return result;
                }
            }
            else
            {
                result.AppendError("表达式exp不允许为空");
                return result;
            }

            #endregion

            StringBuilder sqlBuilder = new StringBuilder("select ");
            if (resolveResult.SqlSelectFields != null && resolveResult.SqlSelectFields.Count() > 0)
            {
                #region 拼接Sql语句

                foreach (var item in resolveResult.SqlSelectFields.OrderBy(d => d.IsModelProperty).OrderBy(d => d.DBFieldAsName))
                {
                    sqlBuilder.Append(item.DBSelectFragment);
                    sqlBuilder.Append(" as ");
                    sqlBuilder.Append("[");
                    sqlBuilder.Append(item.DBFieldAsName);
                    sqlBuilder.Append("],");
                }
                sqlBuilder.Replace(",", "", sqlBuilder.Length - 1, 1);
                sqlBuilder.Append(" from ");
                sqlBuilder.Append("[");
                sqlBuilder.Append(this._dbMappingHandler.GetDbTableName(modelT));
                sqlBuilder.Append("] ");
                if (!string.IsNullOrEmpty(resolveResult.SqlWhereConditionText))
                {
                    sqlBuilder.Append(" where ");
                    sqlBuilder.Append(resolveResult.SqlWhereConditionText);
                }
                if (resolveResult.SqlQuerylParameters != null && resolveResult.SqlQuerylParameters.Count() > 0)
                {
                    foreach (var item in resolveResult.SqlQuerylParameters)
                    {
                        cur_parameter = new SqlParameter(item.Name, item.Value);
                        parameters.Add(cur_parameter);
                    }
                }
                if (!string.IsNullOrEmpty(resolveResult.SqlGroupConditionBuilder))
                {
                    sqlBuilder.Append(" group by ");
                    sqlBuilder.Append(resolveResult.SqlGroupConditionBuilder);
                }
                if (!string.IsNullOrEmpty(resolveResult.SqlOrderConditionText))
                {
                    sqlBuilder.Append(" order by ");
                    sqlBuilder.Append(resolveResult.SqlOrderConditionText);
                }
                sqlBuilder.Append(";");
                //初始化Debug
                result.DebugInit(sqlBuilder, MssqlGrammarRule.C_ParamChar, parameters.ToArray());

                #endregion

                #region 执行Sql语句

                using (DbConnection connection = new SqlConnection(dbString))
                {
                    using (DbCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = sqlBuilder.ToString();
                        if (parameters.Count > 0)
                        {
                            foreach (var item in parameters)
                            {
                                command.Parameters.Add(item);
                            }
                        }
                        //尝试打开数据库链接
                        if (this.TryOpenDbConnection<DbCalculateRecord>(connection, ref result))
                        {
                            //尝试执行语句返回DataReader
                            DbDataReader reader = this.TryExecuteReader<DbCalculateRecord>(command, ref result);
                            if (reader != null && reader.HasRows)
                            {
                                List<DbRowRecord> rowDataList = new List<DbRowRecord>();//设置所有的行数据容器
                                DbRowRecord rowItem = null;//设置行数据对象
                                DbColumnRecord columnItem = null;//列数据对象
                                while (reader.Read())
                                {
                                    rowItem = new DbRowRecord();

                                    //开始遍历所有的列数据
                                    foreach (var item in resolveResult.SqlSelectFields)
                                    {
                                        object objVal = reader[item.DBFieldAsName];
                                        if (objVal != null && objVal != DBNull.Value)
                                        {
                                            columnItem = new DbColumnRecord();
                                            columnItem.Name = item.DBFieldAsName;
                                            columnItem.Value = objVal;

                                            //在行数据对象中装载列数据
                                            rowItem.Add(columnItem);
                                        }
                                    }
                                    rowDataList.Add(rowItem);
                                }
                                result.Record = rowDataList;

                                //释放资源，关闭连结
                                this.DisposeReader(reader);
                            }
                        }
                    }
                }

                #endregion

                return result;
            }
            else
            {
                #region 必须至少指定一个运算模式，例如:Count,Sum,Max,Min等

                result.AppendError("必须至少指定一个运算模式，例如:Count,Sum,Max,Min等");
                return result;

                #endregion
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 尝试打开数据库链接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool TryOpenDbConnection<T>(DbConnection connection, ref T result)
            where T : ResultBase
        {
            bool isOpen = false;
            try
            {
                connection.Open();
                isOpen = true;
            }
            catch (Exception ex)
            {
                isOpen = false;
                result.AppendError("数据库无法打开!");
                result.AppendException(ex);
            }
            return isOpen;
        }

        /// <summary>
        /// 尝试执行DBDataReader,可能返回为null值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private DbDataReader TryExecuteReader<T>(DbCommand command, ref T result)
            where T : ResultBase
        {
            DbDataReader reader = null;
            try
            {
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                reader = null;
                result.AppendError("sql语句执行错误，" + command.CommandText);
                result.AppendException(ex);
            }
            return reader;
        }

        /// <summary>
        /// 执行关闭并且释放资源
        /// </summary>
        /// <param name="reader"></param>
        private void DisposeReader(DbDataReader reader)
        {
            //释放资源，关闭连结
            reader.Dispose();
            reader.Close();
        }

        /// <summary>
        /// Model实体自动填充(请在調用该方法前进行reader.Read()判断)
        /// </summary>
        /// <param name="reader">数据源</param>
        /// <param name="dbModelT">当前dbmodel类型</param>
        /// <param name="selectFields">需要被指定填充的字段</param>
        /// <returns></returns>
        private M AutoFillModel(DbDataReader reader, Type dbModelT, IEnumerable<MssqlSelectField> selectFields)
        {
            bool isCreateInstance = false;
            M model = default(M);

            DbColumnAttribute[] columns = this._dbMappingHandler.GetDbColumnCollection(dbModelT);
            if (null == columns || columns.Length <= 0)
                return model;

            if (null == selectFields || selectFields.Count() <= 0)
                return model;

            //开始循环填充Model中指定要被填充的属性值
            foreach (var item in selectFields)
            {
                if (!columns.Any(d => d.DbColumnName == item.DBFieldAsName))
                    continue;
                if (reader.GetOrdinal(item.DBFieldAsName) < 0)
                    continue;
                object fieldValue = reader[item.DBFieldAsName];
                if (DBNull.Value == fieldValue)
                    continue;

                if (!isCreateInstance)
                {
                    model = new M();
                    isCreateInstance = true;
                }

                DbColumnAttribute cur_column = columns.First(d => d.DbColumnName == item.DBFieldAsName);
                if (null == cur_column)
                    continue;

                PropertyInfo p = dbModelT.GetProperty(cur_column.PropertyNameMapping);
                if (null == p)
                    continue;

                fieldValue = fieldValue.GetType() == typeof(Guid) ? Guid.Parse(fieldValue.ToString()) : Convert.ChangeType(fieldValue, p.PropertyType, default(IFormatProvider));
                p.SetValue(model, fieldValue, null);
            }

            return model;
        }

        /// <summary>
        /// 根据数据库格式类型获取SqlDbType类型
        /// </summary>
        /// <param name="dbtypeName"></param>
        /// <returns></returns>
        private System.Data.SqlDbType GetDbtype(string dbtypeName)
        {
            System.Data.SqlDbType dbType = System.Data.SqlDbType.VarChar;
            bool isFind = true;

            switch (dbtypeName.ToLower().Trim())
            {

                case "bigint":
                    dbType = System.Data.SqlDbType.BigInt;
                    break;
                case "binary":
                    dbType = System.Data.SqlDbType.Binary;
                    break;
                case "bool":
                    dbType = System.Data.SqlDbType.Bit;
                    break;
                case "bit":
                    dbType = System.Data.SqlDbType.Bit;
                    break;
                case "char":
                    dbType = System.Data.SqlDbType.Char;
                    break;
                case "date":
                    dbType = System.Data.SqlDbType.Date;
                    break;
                case "datetime":
                    dbType = System.Data.SqlDbType.DateTime;
                    break;
                case "datetime2":
                    dbType = System.Data.SqlDbType.DateTime2;
                    break;
                case "datetimeoffset":
                    dbType = System.Data.SqlDbType.DateTimeOffset;
                    break;
                case "decimal":
                    dbType = System.Data.SqlDbType.Decimal;
                    break;
                case "numeric":
                    dbType = System.Data.SqlDbType.Decimal;
                    break;
                case "float":
                    dbType = System.Data.SqlDbType.Float;
                    break;
                case "image":
                    dbType = System.Data.SqlDbType.Image;
                    break;
                case "int":
                    dbType = System.Data.SqlDbType.Int;
                    break;
                case "money":
                    dbType = System.Data.SqlDbType.Money;
                    break;
                case "nchar":
                    dbType = System.Data.SqlDbType.NChar;
                    break;
                case "ntext":
                    dbType = System.Data.SqlDbType.NText;
                    break;
                case "nvarchar":
                    dbType = System.Data.SqlDbType.NVarChar;
                    break;
                case "real":
                    dbType = System.Data.SqlDbType.Real;
                    break;
                case "smalldatetime":
                    dbType = System.Data.SqlDbType.SmallDateTime;
                    break;
                case "smallint":
                    dbType = System.Data.SqlDbType.SmallInt;
                    break;
                case "smallmoney":
                    dbType = System.Data.SqlDbType.SmallMoney;
                    break;
                case "structured":
                    dbType = System.Data.SqlDbType.Structured;
                    break;
                case "text":
                    dbType = System.Data.SqlDbType.Text;
                    break;
                case "time":
                    dbType = System.Data.SqlDbType.Time;
                    break;
                case "timestamp":
                    dbType = System.Data.SqlDbType.Timestamp;
                    break;
                case "tinyint":
                    dbType = System.Data.SqlDbType.TinyInt;
                    break;
                case "udt":
                    dbType = System.Data.SqlDbType.Udt;
                    break;
                case "uniqueidentifier":
                    dbType = System.Data.SqlDbType.UniqueIdentifier;
                    break;
                case "varbinary":
                    dbType = System.Data.SqlDbType.VarBinary;
                    break;
                case "varchar":
                    dbType = System.Data.SqlDbType.VarChar;
                    break;
                case "string":
                    dbType = System.Data.SqlDbType.VarChar;
                    break;
                case "variant":
                    dbType = System.Data.SqlDbType.Variant;
                    break;
                case "xml":
                    dbType = System.Data.SqlDbType.Xml;
                    break;
                default:
                    isFind = false;
                    break;
            }

            if (!isFind)
            {
                throw new Exception("未找到类型" + dbtypeName);
            }

            return dbType;
        }

        #endregion

        #region IDbConnectionString

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        string IDbConnectionString.GetConnection()
        {
            return this._dbConnQuery;
        }

        #endregion
    }
}
