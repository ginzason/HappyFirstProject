using Happy.Utility;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Happy.Dac.Hims
{
    public class Dac_Hims_AuthInfo : DbHelper
    {
        /// <summary>
        /// 모든 권한 메뉴 불러오기
        /// </summary>
        /// <param name="au_idx"></param>
        /// <returns></returns>
        public DataSet Select_AuthMenuAll(int au_idx)
        {
            string qry = "SP_MIS_SELECT_AUTH_MENU_ALL";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@AU_IDX", au_idx));
            return SqlGetDataSet(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 권한마스터 불러오기
        /// </summary>
        /// <returns></returns>
        public DataSet Select_AuthMaster()
        {
            string qry = "SP_MIS_SELECT_AUTH_MASTER";
            return SqlGetDataSet(qry, null, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 권한 마스터 저장
        /// </summary>
        /// <param name="au_name">권한명</param>
        /// <param name="create_user">저장자</param>
        /// <returns></returns>
        public int Insert_AuthMaster(string au_name, string create_user)
        {
            string qry = "SP_Mis_INSERT_AUTH_MASTER";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@AU_NAME", au_name));
            ParamList.Add(new SqlParameter("@CREATE_USER", create_user));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 권한마스터 삭제
        /// </summary>
        /// <param name="au_idx"></param>
        /// <returns></returns>
        public int Delete_AuthMaster(int au_idx)
        {
            string qry = "SP_MIS_DELETE_AUTH_MASTER";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@AU_IDX", au_idx));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 권한메뉴삭제
        /// </summary>
        /// <param name="au_idx"></param>
        /// <returns></returns>
        public int Delete_AuthMenu(int au_idx)
        {
            string qry = "SP_Mis_DELETE_AUTH_MENU";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@AU_IDX", au_idx));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 권한메뉴저장
        /// </summary>
        /// <param name="au_idx"></param>
        /// <param name="menu_idx"></param>
        /// <param name="create_user"></param>
        /// <returns></returns>
        public int Insert_AuthMenu(int au_idx, int menu_idx, string create_user)
        {
            string qry = "SP_MIS_INSERT_AUTH_MENU";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@AU_IDX", au_idx));
            ParamList.Add(new SqlParameter("@MENU_IDX", menu_idx));
            ParamList.Add(new SqlParameter("@CREATE_USER", create_user));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 사용자권한 삭제
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int Delete_AuthUser(int au_idx)
        {
            string qry = "SP_MIS_DELETE_AUTH_USER";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@AU_IDX", au_idx));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
        /// <summary>
        /// 사용자권한 저장
        /// </summary>
        /// <param name="au_idx"></param>
        /// <param name="userid"></param>
        /// <param name="create_user"></param>
        /// <returns></returns>
        public int Insert_AuthUser(int au_idx, string userid, string create_user)
        {
            string qry = "SP_MIS_INSERT_AUTH_USER";
            List<SqlParameter> ParamList = new List<SqlParameter>();
            ParamList.Add(new SqlParameter("@AU_IDX", au_idx));
            ParamList.Add(new SqlParameter("@USERID", userid));
            ParamList.Add(new SqlParameter("@CREATE_USER", create_user));
            return SqlExcuteNonQuery(qry, ParamList, CommandType.StoredProcedure);
        }
    }
}
