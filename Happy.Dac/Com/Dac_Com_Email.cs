using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Happy.Utility;

namespace Happy.Dac.Com
{
    public class Dac_Com_Email : DbHelper
    {
        public int Insert_Email(string to, string title, string body, DateTime date, bool result, string userid)
        {
            string qry = "SP_COM_INSERT_EMAIL_HIS";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@MAIL_TO", to));
            ParamList.Add(new SqlParameter("@MAIL_TITLE", title));
            ParamList.Add(new SqlParameter("@MAIL_BODY", body));
            ParamList.Add(new SqlParameter("@MAIL_DATE", date));
            ParamList.Add(new SqlParameter("@MAIL_RESULT", result == true ? "S" : "F"));
            ParamList.Add(new SqlParameter("@USERID", userid));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
    }
}
