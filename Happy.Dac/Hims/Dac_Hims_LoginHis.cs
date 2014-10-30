using Happy.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Happy.Dac.Mis
{
    public class Dac_Hims_LoginHis : DbHelper
    {
        public int Insert_Login_His(string userid, string result, string macaddr, string ipaddr)
        {
            string qry = "SP_MIS_INSERT_LOGIN_HIS";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@USERID", userid));
            ParamList.Add(new SqlParameter("@RESULT", result));
            ParamList.Add(new SqlParameter("@MACADDR", macaddr));
            ParamList.Add(new SqlParameter("@IPADDR", ipaddr));
            return SqlExcuteNonQuery(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        public DataSet Select_Login_His(string id)
        {
            string qry = "SP_MIS_SELECT_LOGIN_HIS";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@USERID", id));
            return SqlGetDataSet(qry, ParamList, CommandType.StoredProcedure);
        }
    }
}
