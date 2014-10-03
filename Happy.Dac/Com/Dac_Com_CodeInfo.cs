using Happy.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Happy.Dac.Com
{
    public class Dac_Com_CodeInfo : DbHelper
    {
        /// <summary>
        /// 공통코드 카테고리 리스트
        /// </summary>
        /// <param name="start">시작</param>
        /// <param name="end">끝</param>
        /// <returns></returns>
        public DataSet Select_Code_Category(int start, int end)
        {
            string qry = "SP_COM_SELECT_COM_CODE_CATEGORY_LIST";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@SNUM", start));
            ParamList.Add(new SqlParameter("@ENUM", end));
            return SqlGetDataSet(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// 공통코드 리스트
        /// </summary>
        /// <param name="cat_idx">카테고리아이디</param>
        /// <param name="start">시작</param>
        /// <param name="end">끝</param>
        /// <returns></returns>
        public DataSet Select_Code_Info(int cat_idx, int start, int end)
        {
            string qry = "SP_COM_SELECT_COM_CODE_MASTER_LIST";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@CAT_IDX", cat_idx));
            ParamList.Add(new SqlParameter("@SNUM", start));
            ParamList.Add(new SqlParameter("@ENUM", end));
            return SqlGetDataSet(qry, ParamList, System.Data.CommandType.StoredProcedure);
        }
        /// <summary>
        /// 공통코드카테고리 저장
        /// </summary>
        /// <param name="cat_name">카테고리명</param>
        /// <param name="create_user"></param>
        /// <returns></returns>
        public int Insert_Code_Category(string cat_name, string create_user)
        {
            string qry = "SP_COM_INSERT_COM_CODE_CATEGORY";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@CAT_NAME", cat_name));
            ParamList.Add(new SqlParameter("@CREATE_USER", create_user));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 공통코드저장
        /// </summary>
        /// <param name="cat_idx">카테고리아이디</param>
        /// <param name="code_name">코드명</param>
        /// <param name="code_desc">코드상세</param>
        /// <param name="create_user"></param>
        /// <returns></returns>
        public int Insert_Code_Master(int cat_idx, string code_name, string code_desc, string create_user)
        {
            string qry = "SP_COM_INSERT_COM_CODE_MASTER";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@CAT_IDX", cat_idx));
            ParamList.Add(new SqlParameter("@CODE_NAME", code_name));
            ParamList.Add(new SqlParameter("@CODE_DESC", code_desc));
            ParamList.Add(new SqlParameter("@CREATE_USER", create_user));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 공통코드 업데이트
        /// </summary>
        /// <param name="code_idx">코드아이디</param>
        /// <param name="cat_idx">카테고리아이디</param>
        /// <param name="code_name">코드명</param>
        /// <param name="code_desc">상세</param>
        /// <param name="update_user"></param>
        /// <returns></returns>
        public int Update_Code_Master(int code_idx, int cat_idx, string code_name, string code_desc, string update_user)
        {
            string qry = "SP_COM_UPDATE_COM_CODE_MASTER";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@CODE_IDX", code_idx));
            ParamList.Add(new SqlParameter("@CAT_IDX", cat_idx));
            ParamList.Add(new SqlParameter("@CODE_NAME", code_name));
            ParamList.Add(new SqlParameter("@CODE_DESC", code_desc));
            ParamList.Add(new SqlParameter("@UPDATE_USER", update_user));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 공통코드 삭제
        /// </summary>
        /// <param name="code_idx"></param>
        /// <returns></returns>
        public int Delete_Code_Master(int code_idx)
        {
            string qry = "SP_COM_DELET_COM_CODE_MASTER";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@CODE_IDX", code_idx));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
    }
}
