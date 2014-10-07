using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Happy.Utility
{
    public class DbHelper
    {
        #region Global Property
        private SqlConnection sqlCon = new SqlConnection();
        private SqlCommand sqlCom = new SqlCommand();
        private SqlDataAdapter sqlAdapeter = new SqlDataAdapter();
        private string connectionString = ConfigurationManager.ConnectionStrings["DBSTRING"].ConnectionString;
        private bool IsLog = ConfigurationManager.AppSettings["DBLOG"] == "Y" ? true : false;
        #endregion

        /// <summary>
        /// 반환값이 없는 쿼리
        /// </summary>
        /// <param name="query">쿼리</param>
        /// <param name="param">파라미터</param>
        /// <param name="type">쿼리타입</param>
        /// <returns></returns>
        protected int SqlExcuteNonQuery(string query, List<SqlParameter> param, CommandType type)
        {
            int result = 0;
            try
            {
                if (IsLog)
                {
                    new CreateLog().WriteDbLog(query, param);
                }
                sqlCon.ConnectionString = connectionString;
                sqlCom.CommandText = query;
                sqlCom.CommandType = type;
                sqlCom.Connection = sqlCon;
                sqlCom.Parameters.Clear();
                if (param != null)
                {
                    foreach (var data in param)
                    {
                        sqlCom.Parameters.Add(data);
                    }
                }

                sqlCon.Open();
                result = sqlCom.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 값 한가지일때
        /// </summary>
        /// <param name="query">쿼리</param>
        /// <param name="param">파라미터</param>
        /// <param name="type">디비타입</param>
        /// <returns></returns>
        protected string SqlGetScarlar(string query, List<SqlParameter> param, CommandType type)
        {
            string result = string.Empty;
            try
            {
                if (IsLog)
                {
                    new CreateLog().WriteDbLog(query, param);
                }
                sqlCon.ConnectionString = connectionString;
                sqlCom.CommandText = query;
                sqlCom.CommandType = type;
                sqlCom.Connection = sqlCon;
                sqlCom.Parameters.Clear();
                if (param != null)
                {
                    foreach (var data in param)
                    {
                        sqlCom.Parameters.Add(data);
                    }
                }

                sqlCon.Open();
                result = sqlCom.ExecuteScalar().ToString();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 데이터셋
        /// </summary>
        /// <param name="query">쿼리</param>
        /// <param name="param">파라미터</param>
        /// <param name="type">디비타입</param>
        /// <returns></returns>
        protected DataSet SqlGetDataSet(string query, List<SqlParameter> param, CommandType type)
        {
            DataSet ds = new DataSet();
            try
            {
                if (IsLog)
                {
                    new CreateLog().WriteDbLog(query, param);
                }
                sqlCon.ConnectionString = connectionString;
                sqlCom.CommandText = query;
                sqlCom.CommandType = type;
                sqlCom.Connection = sqlCon;
                sqlCom.Parameters.Clear();
                if (param != null)
                {
                    foreach (var data in param)
                    {
                        sqlCom.Parameters.Add(data);
                    }
                }
                sqlAdapeter.SelectCommand = sqlCom;
                sqlCon.Open();
                sqlAdapeter.Fill(ds);
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
    }
}
