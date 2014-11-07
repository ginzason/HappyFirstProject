using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Happy.Utility;

namespace Happy.Dac.Web
{
    public class Dac_Web_Weekend : DbHelper
    {
        /// <summary>
        /// 주말설정.
        /// </summary>
        /// <param name="yyyy">년</param>
        /// <param name="mm">월</param>
        /// <param name="dd">일</param>
        /// <param name="createUser"></param>
        /// <returns></returns>
        public int Insert_Weekend(string yyyy, string mm, string dd, string createUser)
        {
            string qry = "SP_WEB_INSERT_CAL_WEEKEND";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@YYYY", yyyy));
            ParamList.Add(new SqlParameter("@MM", mm));
            ParamList.Add(new SqlParameter("@DD", dd));
            ParamList.Add(new SqlParameter("@CREATE_USER", createUser));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        public int Delete_Weekend(string yyyy)
        {
            string qry = "SP_WEB_DELETE_CAL_WEEKEND";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@YYYY", yyyy));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// 주말가져오기
        /// </summary>
        /// <param name="yyyy">년도</param>
        /// <returns></returns>
        public DataSet Select_Weekend(string yyyy)
        {
            string qry = "SP_WEB_SELECT_CAL_WEEKEND";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@YYYY", yyyy));
            return SqlGetDataSet(qry, ParamList, CommandType.StoredProcedure);
        }
    }
}
