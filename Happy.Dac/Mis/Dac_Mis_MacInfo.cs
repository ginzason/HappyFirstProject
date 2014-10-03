using Happy.Utility;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Happy.Dac.Hers
{
    public class Dac_Mis_MacInfo : DbHelper
    {
        /// <summary>
        /// Mac 저장
        /// </summary>
        /// <param name="macAddr"></param>
        /// <param name="macName"></param>
        /// <returns></returns>
        public int Insert_Mac_Info(string macAddr, string macName)
        {
            string qry = "SP_MIS_INSERT_MAC_INFO";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@MACADDR", macAddr));
            ParamList.Add(new SqlParameter("@MACNAME", macName));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// Mac삭제
        /// </summary>
        /// <param name="macAddr"></param>
        /// <returns></returns>
        public int Delete_Mac_Info(string macAddr)
        {
            string qry = "SP_MIS_INSERT_MENU_INFO";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@MACADDR", macAddr));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// Mac정보
        /// </summary>
        /// <returns></returns>
        public DataSet Select_Mac_Info()
        {
            string qry = "SP_MIS_SELECT_MAC_INFO";
            return SqlGetDataSet(qry, null, CommandType.StoredProcedure);
        }
    }
}
