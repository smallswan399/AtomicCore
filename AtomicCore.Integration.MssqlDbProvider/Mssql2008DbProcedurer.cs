using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using AtomicCore.DbProvider;

namespace AtomicCore.Integration.MssqlDbProvider
{
    /// <summary>
    /// 执行Mssql的存储过程
    /// </summary>
    public class Mssql2008DbProcedurer : IDbProcedurer
    {
        #region Constructors

        /// <summary>
        /// 数据库链接字符串 
        /// </summary>
        private string _dbConnString = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbConnString">链接字符串</param>
        public Mssql2008DbProcedurer(string dbConnString)
        {
            this._dbConnString = dbConnString;
        }

        #endregion

        #region IBizSqlProcedurer

        /// <summary>
        /// 在当前数据源下执行脚本或命令
        /// </summary>
        /// <param name="inputData">输入的参数对象</param>
        /// <returns></returns>
        public DbCalculateRecord Execute(DbExecuteInputBase inputData)
        {
            DbCalculateRecord result = new DbCalculateRecord();
            if (string.IsNullOrEmpty(inputData.CommandText))
            {
                result.AppendError("执行的脚本命令为空，请传入命令后再调用执行");
                return result;
            }

            //执行读取参数
            List<SqlParameter> sqlParameters = null;
            SqlParameter param = null;
            IEnumerable<object> objParams = inputData.GetParameterCollection();
            IEnumerable<MssqlParameterDesc> parameters = null;
            if (null != objParams)
            {
                parameters = objParams.Cast<MssqlParameterDesc>();
            }

            if (parameters != null && parameters.Count() > 0)
            {
                sqlParameters = new List<SqlParameter>();
                foreach (var msParam in parameters)
                {
                    if (null != msParam)
                    {
                        param = new SqlParameter(msParam.Name, msParam.Value);
                        param.Direction = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), Convert.ToInt32(msParam.Direction).ToString());
                        if (msParam.Size > 0)
                            param.Size = msParam.Size;
                        if (msParam.Precision > 0)
                            param.Precision = msParam.Precision;
                        if (msParam.Scale > 0)
                            param.Scale = msParam.Scale;

                        sqlParameters.Add(param);
                    }
                }
            }
            //Debug初始化
            result.DebugInit(new StringBuilder(inputData.CommandText), MssqlGrammarRule.C_ParamChar, null == sqlParameters ? null : sqlParameters.ToArray());

            //执行数据库查询
            using (DbConnection connection = new SqlConnection(this._dbConnString))
            {
                using (DbCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandTimeout = 0;
                    command.CommandText = inputData.CommandText;
                    command.CommandType = inputData.CommandType;
                    if (sqlParameters != null && sqlParameters.Count > 0)
                    {
                        command.Parameters.AddRange(sqlParameters.ToArray());
                    }
                    //尝试打开数据库链接
                    if (this.TryOpenDbConnection<DbCalculateRecord>(connection, ref result))
                    {
                        if (inputData.HasReturnRecords)
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
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        object objVal = reader.GetValue(i);
                                        if (objVal != null && objVal != DBNull.Value)
                                        {
                                            columnItem = new DbColumnRecord();
                                            columnItem.Name = reader.GetName(i);
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
                        else
                        {
                            int i = command.ExecuteNonQuery();

                            result.Record = new List<DbRowRecord>()
                            {
                                new DbRowRecord()
                                {
                                    new DbColumnRecord()
                                    {
                                        Name = string.Empty,
                                        Value= i
                                    }
                                }
                            };
                        }

                        //将存储过程的输出参数返回
                        if (null != parameters && parameters.Count() > 0)
                        {
                            IEnumerable<MssqlParameterDesc> outParams = parameters.Where(d => d.Direction == MssqlParameterDirection.Output || d.Direction == MssqlParameterDirection.InputOutput);
                            if (null != outParams && outParams.Count() > 0)
                            {
                                foreach (var item in outParams)
                                {
                                    SqlParameter dbParam = sqlParameters.FirstOrDefault(d => d.ParameterName == item.Name);
                                    if (null != dbParam)
                                    {
                                        item.Value = dbParam.Value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
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
        public void DisposeReader(DbDataReader reader)
        {
            //释放资源，关闭连结
            using (reader as IDisposable) { }
        }

        #endregion
    }
}
