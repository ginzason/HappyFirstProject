using System.Collections.Generic;
using System.Data.SqlClient;
using Happy.Utility;

namespace Happy.Dac
{
    public class Test : DbHelper
    {
        protected List<SqlParameter> ParamList = new List<SqlParameter>();
        public object test()
        {
            string qry = "SP_TEST";
            ParamList.Clear();
            ParamList.Add(new SqlParameter("NAME", "tomochan"));
            ParamList.Add(new SqlParameter("AGE",1));

            return SqlGetScarlar(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
    }
}
